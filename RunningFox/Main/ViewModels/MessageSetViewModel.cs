using GalaSoft.MvvmLight.Command;
using Main.Models;
using Main.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
   public class MessageSetViewModel:NotifyService
    {
        private ObservableCollection<MessageTable> _messageCollection;

        public ObservableCollection<MessageTable> MessageCollection
        {
            get { return _messageCollection; }
            set { _messageCollection = value; OnPropertyChanged(); }
        }


        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            () =>
            {
                _navigationService.NavigateTo("MainPage");
            }
            ));
    
        private IFrameNavigationService _navigationService;
        public MessageSetViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
