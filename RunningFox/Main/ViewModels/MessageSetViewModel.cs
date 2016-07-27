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

        

        private RelayCommand _addMessageCommand;
        public RelayCommand AddMessageCommand => _addMessageCommand ?? (_addMessageCommand = new RelayCommand(
            () =>
            {
                _navigationService.NavigateTo("NewMessageEdit");
            }
            ));

        
        /// <summary>
        /// Remove selected message from the list
        /// </summary>
         private RelayCommand<MessageTable> _removeMessageCommand;
         public RelayCommand<MessageTable> RemoveMessageCommand => _removeMessageCommand ?? (_removeMessageCommand = new RelayCommand<MessageTable>(
            (MessageTable messageTable) =>
            {
                MessageCollection.Remove(messageTable);    
            }
            ));

        

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        


        private IFrameNavigationService _navigationService;
        public MessageSetViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
