using DenunciadoApp.Models;
using DenunciadoApp.Pages;
using DenunciadoApp.ViewModels;
using System.Threading.Tasks;

namespace DenunciadoApp.Services
{

    public class NavigationService
    {
        //generico para paginas que no requieran parametros
        public async Task Navigate(string pageName)
        {
            var mainViewModel = MainViewModel.GetInstance();

            switch (pageName)

            {

                case "NewComplaintPage":
                    //Los saco de aqui para poder usarlo en cada caso
                    //var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.NewComplaint = new NewComplaintViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new NewComplaintPage());
                    break;
                //case "EditComplaintPage":
                //    mainViewModel.EditComplaint = new EditComplaintViewModel();
                //    await App.Current.MainPage.Navigation.PushAsync(new EditComplaintPage());
                //    break;
                default:
                    break;
            }

        }


        public async Task Back()

        {
            await App.Current.MainPage.Navigation.PopAsync();

        }

        public async Task EditComplaint(Complaint Complaint)

        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditComplaint = new EditComplaintViewModel(Complaint);
            await App.Current.MainPage.Navigation.PushAsync(new EditComplaintPage());

        }



    }
}
