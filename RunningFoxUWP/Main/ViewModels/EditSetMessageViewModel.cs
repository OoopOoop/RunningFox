using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Main.ViewModels
{
    public class EditSetMessageViewModel : ViewModelBase
    {
        private ObservableCollection<MessageTable> _messageTableCollection;

        public ObservableCollection<MessageTable> MessageTableCollection
        {
            get { return _messageTableCollection; }
            set { _messageTableCollection = value; OnPropertyChanged(); }
        }

        private bool _isRepeating;

        public bool IsRepeating
        {
            get { return _isRepeating; }
            set { _isRepeating = value; OnPropertyChanged(); }
        }

        public EditSetMessageViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;
            MessageTableCollection = new ObservableCollection<MessageTable>();

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
            var messageSet = new MessageSetTable() { Description = ProgramDescription, MessageCollection = MessageTableCollection, SetID = new Guid() };

            _navigationService.NavigateTo("MainPage");
            Messenger.Default.Send(messageSet);
            ProgramDescription = string.Empty;
        }

        private RelayCommand<MessageTable> _editMessageTableCommand;
        public RelayCommand<MessageTable> EditMessageTableCommand => _editMessageTableCommand ?? (_editMessageTableCommand = new RelayCommand<MessageTable>(ammendMessage));

        private void ammendMessage(MessageTable table)
        {
            _navigationService.NavigateTo("EditMessage");
            Messenger.Default.Send(table);
        }


        private RelayCommand<MessageTable> _removeSelectedMessageTable;
        public RelayCommand<MessageTable> RemoveSelectedMessageTable => _removeSelectedMessageTable ?? (_removeSelectedMessageTable = new RelayCommand<MessageTable>(removeMessageTable));

        private void removeMessageTable(MessageTable messageToRemove)
        {
           if(messageToRemove!=null)
            {
                MessageTableCollection.Remove(messageToRemove);
            }
        }

        private RelayCommand<MessageTable> _moveUpMessageTableCommand;
        public RelayCommand<MessageTable> MoveUpMessageTableCommand => _moveUpMessageTableCommand ?? (_moveUpMessageTableCommand = new RelayCommand<MessageTable>(moveMessageUp));

        private void moveMessageUp(MessageTable messageToMove)
        {
            //TODO: keep items selected in the listview while moving them up and down
            if (messageToMove != null)
            {
                int index = MessageTableCollection.IndexOf(messageToMove);

                if(index!=0)
                {
                    var previousMessage = MessageTableCollection[index - 1];
                    MessageTableCollection[index - 1] = messageToMove;
                    MessageTableCollection[index] = previousMessage;
                }
            }
        }



        private RelayCommand<MessageTable> _moveDownMessageTableCommand;
        public RelayCommand<MessageTable> MoveDownMessageTableCommand => _moveDownMessageTableCommand ?? (_moveDownMessageTableCommand = new RelayCommand<MessageTable>(moveMessageDown));

        private void moveMessageDown(MessageTable messageToMove)
        {
            if (messageToMove != null)
            {
                int index = MessageTableCollection.IndexOf(messageToMove);
                

                if (index != MessageTableCollection.Count-1)
                {
                    var previousMessage = MessageTableCollection[index + 1];
                    MessageTableCollection[index + 1] = messageToMove;
                    MessageTableCollection[index] = previousMessage;
                }
            }
        }

        
        /// <summary>
        /// Receive collection of messages, if MessageTableCollection collection already contains any of messages, update the message, if not- add it to the collection.
        /// </summary>
        private void getMessages()
        {
            Messenger.Default.Register<ObservableCollection<MessageTable>>(
            this,
            messageCollection =>
            {
                foreach (MessageTable message in messageCollection)
                {
                    var test = MessageTableCollection.Where(x => x.GuidID == message.GuidID).FirstOrDefault();

                    if (test != null)
                    {
                        test.MessageText = message.MessageText;
                        test.DisplayTime = message.DisplayTime;
                    }
                    else
                    {
                        MessageTableCollection.Add(message);
                    }
                }
            });
        }
    }
}