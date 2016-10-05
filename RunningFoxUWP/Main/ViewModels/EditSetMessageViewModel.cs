using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
    public class EditSetMessageViewModel : ViewModelBase
    {        private ObservableCollection<MessageTable> _messages;
        public ObservableCollection<MessageTable> Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged(); }
        }



        public EditSetMessageViewModel(INavigationService navigationService)
        {
           base._navigationService = navigationService;
            Messages = new ObservableCollection<MessageTable>();

            getMessages();
        }



        private string _programDescription;
        public string ProgramDescription
        {
            get { return _programDescription; }
            set { _programDescription = value; OnPropertyChanged(); }
        }

        

        private RelayCommand _saveNewProgramCommand;
        public RelayCommand SaveNewProgramCommand => _saveNewProgramCommand ?? (_saveNewProgramCommand = new RelayCommand(saveNewSet));


        private void saveNewSet()
        {
            var messageSet = new MessageSetTable() { Description = ProgramDescription, MessageCollection = Messages,  SetID=new Guid()};


          
            _navigationService.NavigateTo("MainPage");
            Messenger.Default.Send(messageSet);
            Messages.Clear();
            ProgramDescription = string.Empty;
        }

        
        private void getMessages()
        {
           Messenger.Default.Register<MessageTable>(
           this,
           message =>
           {
               Messages.Add(message);
           });
        }
    }
}
