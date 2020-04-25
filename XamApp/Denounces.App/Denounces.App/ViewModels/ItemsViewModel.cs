using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Denounces.App.Models;
using Denounces.App.Views;
using Denounces.App.Services;
using System.Collections.Generic;
using Denounces.Web.Models;

namespace Denounces.App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private List<ProposalResponse> _Proposals;

        public ItemsViewModel(
            IApiService apiService)
        {
            _apiService = apiService;
            Title = "Denuncias";
            LoadProposalsAsync();
        }

        public List<ProposalResponse> Proposals
        {
            get => _Proposals;
            set => SetProperty(ref _Proposals, value);
        }

        private async void LoadProposalsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<ProposalResponse>(
                url,
                "/api",
                "/Proposals");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Proposals = (List<ProposalResponse>)response.Result;
        }
    }
}