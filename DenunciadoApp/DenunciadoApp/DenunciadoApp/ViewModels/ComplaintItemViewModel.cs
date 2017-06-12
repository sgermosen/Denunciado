using ComplaintsApp.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using DenunciadoApp.Services;

namespace DenunciadoApp.ViewModels
{
    public class ComplaintItemViewModel : Complaint
    {
        private NavigationService navigationService;

        //constructuro
        public ComplaintItemViewModel()
        {
            navigationService = new Services.NavigationService();
        }

        public ICommand EditComplaintCommand { get { return new RelayCommand(EditComplaint); } }

        private async void EditComplaint()
        {
            await navigationService.EditComplaint(this);
        }
    }
}
