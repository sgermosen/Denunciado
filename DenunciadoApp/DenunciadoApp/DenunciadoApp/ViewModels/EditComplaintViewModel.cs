using ComplaintsApp.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using ComplaintsApp.Services;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using Plugin.Media;
using ComplaintsApp.Classes;

namespace DenunciadoApp.ViewModels
{
    public class EditComplaintViewModel : Complaint, INotifyPropertyChanged //hereda de la clase Complaint y hereda sus miembros
    {
        #region Atributos
        private DialogService dialogService;
        private ApiService apiService;
        private NavigationService navigationService;
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Propiedades
        public ImageSource ImageSource
        {
            set {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
            get {
                return imageSource;
            }
        }

        public bool IsRunning
        {
            set {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get {
                return isEnabled;
            }
        }



        #endregion

        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructores
        public EditComplaintViewModel(Complaint Complaint)
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            Image = Complaint.Image;
            IsActive = Complaint.IsActive;
            Observation = Complaint.Observation;
            LastPurchase = Complaint.LastPurchase;
            Description = Complaint.Description;
            Price = Complaint.Price;
            ComplaintId = Complaint.ComplaintId;

            isEnabled = true;
        }
        #endregion
        //constructor
        #region Comandos
        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }

        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "Aceptar");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize = PhotoSize.Small,
            });

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() => {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
        }

        public ICommand SaveComplaintCommand { get { return new RelayCommand(SaveComplaint); } }

        private async void SaveComplaint()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "Debe entroducir una descripcion");
                return;
            }
            if (Price <= 0)
            {
                await dialogService.ShowMessage("Error", "Debe entroducir un numero mayor de cero en el precio");
                return;
            }

            var imageArray = FilesHelper.ReadFully(file.GetStream());
            file.Dispose(); //liberando memoria

            var Complaint = new Complaint
            {
                ComplaintId = ComplaintId,
                Image = Image,
                Description = Description,
                Price = Price,
                IsActive = IsActive,
                LastPurchase = LastPurchase,
                Observation = Observation,
                ImageArray = imageArray,
            };
            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.Put("http://psComplaintsback.azurewebsites.net", "/api", "/Complaints", Complaint);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await navigationService.Back();
        }
        public ICommand DeleteComplaintCommand { get { return new RelayCommand(DeleteComplaint); } }

        private async void DeleteComplaint()
        {
            var answer = await dialogService.ShowConfirm("Confirmar", "Estas Seguro que deseas eliminar esto?");

            if (!answer)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.Delete("http://psComplaintsback.azurewebsites.net", "/api", "/Complaints", this);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await navigationService.Back();
        }
        #endregion


    }
}
