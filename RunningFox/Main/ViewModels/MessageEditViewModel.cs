using GalaSoft.MvvmLight.Command;
using Main.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
   public class MessageEditViewModel:NotifyService
    {
        private IFrameNavigationService _navigationService;
        

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            () =>
            {
                _navigationService.NavigateTo("EditMessageSet");
            }
            ));


        public MessageEditViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
