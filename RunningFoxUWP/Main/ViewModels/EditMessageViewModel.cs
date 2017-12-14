using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Main.ViewModels
{
    public class EditMessageViewModel : ViewModelBase
    {
        private readonly TimeSpan _defaultTime=new TimeSpan(00, 05, 00);
        private readonly string _defaultMessage=string.Empty;
        private string _confirmMessage;
        private string _messageToDisplay;
        private bool _isRepeatEnabled;
        private RelayCommand _saveSingleMessageCommand;
        private RelayCommand _duplicatePreviousMessageCommand;
        private RelayCommand<object> _removeMessageFromList;
        private RelayCommand _saveMessageCommand;
        private TimeSpan _time;
        private MessageTable CurrentMessage { get; set; }
        
        public bool IsRepeatEnabled
        {
            get => _isRepeatEnabled;
            set { _isRepeatEnabled = value; OnPropertyChanged();}
        }
        
        public RelayCommand SaveSingleMessageCommand => _saveSingleMessageCommand ?? (_saveSingleMessageCommand = new RelayCommand(SaveProgram));
        
        public RelayCommand DuplicatePreviousMessagesCommand => _duplicatePreviousMessageCommand ?? (_duplicatePreviousMessageCommand = new RelayCommand(DuplicatePreviousMessage));
        
        public RelayCommand<object> RemoveMessageFromList => _removeMessageFromList ?? (_removeMessageFromList = new RelayCommand<object>(DeleteMessage));
        
        public RelayCommand SaveMessageCommand => _saveMessageCommand ?? (_saveMessageCommand = new RelayCommand(SaveNewMessage));

        public ObservableCollection<MessageTable> PopulatedMessages { get; private set; }
        
        public string ConfirmMessage
        {
            get => _confirmMessage;
            private set { _confirmMessage = value; OnPropertyChanged(); }
        }
        
        public string MessageToDisplay
        {
            get => _messageToDisplay;
            set { _messageToDisplay = value; OnPropertyChanged(); }
        }
        
        public TimeSpan Time
        {
            get => _time;
            set { _time = value; OnPropertyChanged(); }
        }
        
        private void DuplicatePreviousMessage()
        {
            var messageToCopy= new MessageTable();

            if (PopulatedMessages.Count != 0)
            {
                messageToCopy.DisplayTime = PopulatedMessages[PopulatedMessages.Count - 1].DisplayTime;
                messageToCopy.GuidID = Guid.NewGuid();
                messageToCopy.MessageText = PopulatedMessages[PopulatedMessages.Count - 1].MessageText;
                PopulatedMessages.Add(messageToCopy);
                return;
            }

            ConfirmNewMessage(CreateNewMessage());
        }

        private void ConfirmNewMessage(MessageTable message)
        {
            PopulatedMessages.Add(message);
            Time = _defaultTime;
            MessageToDisplay = _defaultMessage;
            ConfirmMessage = $"Message:  {message.MessageText} was added, duration: { message.DisplayTimeText}";
        }
        
        private void DeleteMessage(object message)
        {
            var messageTable = (MessageTable)message;
            if (messageTable != null)
            {
                PopulatedMessages.Remove(PopulatedMessages.FirstOrDefault(i => i.GuidID == messageTable.GuidID));
            }

            IsRepeatEnabled = PopulatedMessages.Count > 0;
            ConfirmMessage = $"Message:  {messageTable?.MessageText} was removed, duration: { messageTable?.DisplayTimeText}";
        }

        private void SaveNewMessage()
        {
            ConfirmNewMessage(CreateNewMessage());
            CurrentMessage = null;
        }

        private MessageTable CreateNewMessage()
        {
            if (CurrentMessage == null)
            {
                CurrentMessage = new MessageTable { GuidID = Guid.NewGuid() };
            }

            CurrentMessage.DisplayTime = Time;
            CurrentMessage.MessageText = string.IsNullOrEmpty(this.MessageToDisplay) ? "test" : MessageToDisplay;
            IsRepeatEnabled = true;
            return CurrentMessage;
        }

        private void SaveProgram()
        {
            if (PopulatedMessages.Count == 0 || Time != _defaultTime || MessageToDisplay != _defaultMessage)
            {
                PopulatedMessages.Add(CreateNewMessage());
            }
            
            _navigationService.NavigateTo("EditSet");
            Messenger.Default.Send(PopulatedMessages);
        }
        
        public EditMessageViewModel(INavigationService navigationService)
        {
            PopulatedMessages = new ObservableCollection<MessageTable>();
            _navigationService = navigationService;
            Time = _defaultTime;
            MessageToDisplay = string.Empty;
            IsRepeatEnabled = false;
            ReceivedMessageToEdit();
        }
        
        private void ReceivedMessageToEdit()
        {
            Messenger.Default.Register<MessageTable>(
            this,
            message =>
            {
                MessageToDisplay = message.MessageText;
                Time = message.DisplayTime;
                CurrentMessage = message;
            });
        }
    }
} //TODO: bind buttons enabled/disabled when deleting, edititing without selection, moving last item down or first item up.