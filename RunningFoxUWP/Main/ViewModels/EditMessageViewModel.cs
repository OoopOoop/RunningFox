using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Linq;

namespace Main.ViewModels
{
    public class EditMessageViewModel : ViewModelBase
    {
        private string _confirmMessage;
        private string _messageToDisplay=string.Empty;
        private bool _isRepeatEnabled;
        private RelayCommand _saveSingleMessageCommand;
        private RelayCommand _duplicatePreviousMessageCommand;
        private RelayCommand<object> _removeMessageFromList;
        private RelayCommand _saveMessageCommand;
        private TimeSpan _time= new TimeSpan(00, 05, 00);
        private IMessageTableCollection _messageCollection;
        private MessageTable _selectedMessage;
        
        public MessageTable SelectedMessage
        {
            get => _selectedMessage;
            set
            {
                if (_selectedMessage != value)
                {
                    _selectedMessage = value;
                    if (_selectedMessage != null)
                    {
                        MessageToDisplay = _selectedMessage.MessageText;
                        Time = _selectedMessage.DisplayTime;
                    }
                    OnPropertyChanged();
                }
            }
        }
        
        public bool IsRepeatEnabled
        {
            get => _isRepeatEnabled;
            set { _isRepeatEnabled = value; OnPropertyChanged();}
        }

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

        public IMessageTableCollection MessageCollection
        {
            get => _messageCollection;
            set { _messageCollection = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveSingleMessageCommand => _saveSingleMessageCommand ?? (_saveSingleMessageCommand = new RelayCommand(SaveProgram));
        
        public RelayCommand DuplicatePreviousMessagesCommand => _duplicatePreviousMessageCommand ?? (_duplicatePreviousMessageCommand = new RelayCommand(DuplicatePreviousMessage));
        
        public RelayCommand<object> RemoveMessageFromList => _removeMessageFromList ?? (_removeMessageFromList = new RelayCommand<object>(DeleteMessage));
        
        public RelayCommand SaveMessageCommand => _saveMessageCommand ?? (_saveMessageCommand = new RelayCommand(SaveSingleMessage));
     
        public EditMessageViewModel(
            INavigationService navigationService,
            IMessageTableCollection messageCollection)
        {
            _navigationService = navigationService;
            MessageCollection = messageCollection;
            
            CheckIfEdit();
            IsRepeatEnabled = true;
        }
        
        private void CheckIfEdit()
        {
            if (_messageCollection.SelectedMessage == null)
            {
                return;
            }

            SelectedMessage = _messageCollection.SelectedMessage;
            Time = _messageCollection.SelectedMessage.DisplayTime;
            MessageToDisplay = _messageCollection.SelectedMessage.MessageText;
        }

        private void DuplicatePreviousMessage()
        {
            if (MessageCollection.Messages.Count == 0)
            {
                return;
            }
            var duplicatemessage = new MessageTable
            {
                DisplayTime = MessageCollection.Messages[MessageCollection.Messages.Count - 1].DisplayTime,
                GuidId = Guid.NewGuid(),
                MessageText = MessageCollection.Messages[MessageCollection.Messages.Count - 1].MessageText
            };

            MessageCollection.Messages.Add(duplicatemessage);
            SendConfirmMessage(duplicatemessage.DisplayTimeText, duplicatemessage.MessageText);
            MessageToDisplay = string.Empty;
            Time = new TimeSpan();
        }

        private void SendConfirmMessage(string time, string message)
        {
            ConfirmMessage = $"Message:  {message} was added, duration: { time}";
        }
        
        private void DeleteMessage(object message)
        {
            var messageTable = (MessageTable)message;
            if (messageTable != null)
            {
                MessageCollection.Messages.Remove(MessageCollection.Messages.FirstOrDefault(i => i.GuidId == messageTable.GuidId));
            }

            IsRepeatEnabled = MessageCollection.Messages.Count > 0;
            ConfirmMessage = $"Message:  {messageTable?.MessageText} was removed, duration: { messageTable?.DisplayTimeText}";
        }

        private void SaveSingleMessage()
        {
            if (SelectedMessage != null)
            {
                ModifyMessage(SelectedMessage);
            }

            else
            {
                var newMessage = CreateSingleMessage();
                MessageCollection.Messages.Add(newMessage);
                SendConfirmMessage(newMessage.DisplayTimeText, newMessage.MessageText);
            }

            MessageToDisplay = string.Empty;
            Time = new TimeSpan();
            SelectedMessage = null;
        }

        private void ModifyMessage(MessageTable message)
        {
            int index = MessageCollection.Messages.IndexOf(message);
            MessageCollection.Messages[index].MessageText = MessageToDisplay;
            MessageCollection.Messages[index].DisplayTime = Time;
        }

        private MessageTable CreateSingleMessage()
        {
            return new MessageTable
            {
                DisplayTime = Time,
                MessageText = string.IsNullOrEmpty(this.MessageToDisplay) ? "run for" : MessageToDisplay,
                GuidId = _messageCollection.SelectedMessage?.GuidId ?? Guid.NewGuid()
            };
        }

        private void SaveProgram()
        {
            _navigationService.NavigateTo("EditSet");
        }
    }
} 