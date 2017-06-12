using ComplaintsApp.Models;
using ComplaintsApp.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DenunciadoApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Atributos
        private ApiService apiService;
        private NavigationService navigationService;
        private DialogService dialogService;
        private bool isRefreshing;

        #endregion

        #region Propiedades
        public ObservableCollection<ComplaintItemViewModel> Complaints { get; set; }
        public NewComplaintViewModel NewComplaint { get; set; }
        public EditComplaintViewModel EditComplaint { get; set; }

        public bool IsRefreshing
        {
            set {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get {
                return isRefreshing;
            }
        }

        #endregion

        #region Contructores
        public MainViewModel()
        {
            //singleton
            instance = this;

            //servicios
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();

            //viwe models
            Complaints = new ObservableCollection<ComplaintItemViewModel>();

            //loading datas no la necesito mas porque la instancio en el onapearing
            // LoadComplaints();
        }


        #endregion

        #region Metodos
        private async void LoadComplaints()
        {
            // var Complaints = await apiService.Get<Complaint>("http://psComplaintsback.azurewebsites.net", "/api", "/Complaints");
            var response = await apiService.Get<Complaint>("http://psComplaintsback.azurewebsites.net", "/api", "/Complaints");
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadComplaints((List<Complaint>)response.Result);
        }

        private void ReloadComplaints(List<Complaint> Complaints)
        {
            Complaints.Clear();
            //foreach (var Complaint in Complaints)
            foreach (var Complaint in Complaints.OrderBy(f => f.Description))
            {
                Complaints.Add(new ComplaintItemViewModel
                {
                    Description = Complaint.Description,
                    ComplaintId = Complaint.ComplaintId,
                    Price = Complaint.Price,
                    Image = Complaint.Image,
                    IsActive = Complaint.IsActive,
                    LastPurchase = Complaint.LastPurchase,
                    Observation = Complaint.Observation,
                });
            }
        }
        #endregion

        #region Comandos
        public ICommand AddComplaintCommand { get { return new RelayCommand(AddComplaint); } }
        public ICommand RefreshComplaintCommand { get { return new RelayCommand(RefreshComplaint); } }

        private void RefreshComplaint()
        {
            IsRefreshing = true;
            LoadComplaints();
            IsRefreshing = false;
        }

        private async void AddComplaint()
        {
            await navigationService.Navigate("NewComplaintPage");

        }

        #endregion
        //usar la main view model desdeotras clases
        #region Singleton
        private static MainViewModel instance;



        public static MainViewModel GetInstance()

        {
            if (instance == null)

            {
                instance = new MainViewModel();

            }
            return instance;

        }

        #endregion

        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}
