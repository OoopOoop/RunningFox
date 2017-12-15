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
        private Guid TableSetGuidId;

        private bool _isSetToRepeat;
        public bool IsSetToRepeat
        {
            get { return _isSetToRepeat; }
            set { _isSetToRepeat = value; OnPropertyChanged(); }
        }


        private int _messageListSelectdIndex;
        public int MessageListSelectdIndex
        {
            get { return _messageListSelectdIndex; }
            set { _messageListSelectdIndex = value;OnPropertyChanged(); }
        }
        
        //private ObservableCollection<MessageTable> MessageCollection;
        //public ObservableCollection<MessageTable> MessageTableCollection
        //{
        //    get { return MessageCollection; }
        //    set { MessageCollection = value; OnPropertyChanged(); }
        //}

        private bool _isRepeating;
        public bool IsRepeating
        {
            get => _isRepeating;
            set { _isRepeating = value; OnPropertyChanged(); }
        }


        private IMessageTableCollection _messageCollection;
        public IMessageTableCollection MessageCollection
        {
            get => _messageCollection;
            set { _messageCollection = value; OnPropertyChanged(); }
        }
        
        private readonly IMessageSetTable _messageSetTable;

        private RelayCommand<MessageTable> _moveUpMessageTableCommand;
        public RelayCommand<MessageTable> MoveUpMessageTableCommand => _moveUpMessageTableCommand ?? (_moveUpMessageTableCommand = new RelayCommand<MessageTable>(MoveMessageUp, CanMoveMessageUp));



        public EditSetMessageViewModel(
            INavigationService navigationService, 
            IMessageTableCollection messageCollection,
            IMessageSetTable messageSetTable)
        {
          
            base._navigationService = navigationService;
            _messageSetTable = messageSetTable;
            MessageCollection = messageCollection;
  
           // GetMessages();
           // getMessageSet();
        }


        private string _programDescription;
        public string ProgramDescription
        {
            get => _programDescription;
            set { _programDescription = value; OnPropertyChanged(); }
        }
        
        private RelayCommand _saveNewProgramCommand;
        public RelayCommand SaveNewProgramCommand => _saveNewProgramCommand ?? (_saveNewProgramCommand = new RelayCommand(SaveNewMessageSet));

        private RelayCommand<MessageTable> _moveDownMessageTableCommand;
        public RelayCommand<MessageTable> MoveDownMessageTableCommand => _moveDownMessageTableCommand ?? (_moveDownMessageTableCommand = new RelayCommand<MessageTable>(MoveMessageDown, CanMoveMessageDown));
    
        private void SaveNewMessageSet()
        {
            SetMessagesSortOrder();

                var messageSet = new MessageSetTable
                {
                    Description = ProgramDescription,
                    MessageCollection = MessageCollection.Messages,
                    SetId = TableSetGuidId == Guid.Empty?Guid.NewGuid(): TableSetGuidId,
                    SetToRepeat = IsRepeating,  
                    MessagesTotalCount = MessageCollection.Messages.Count,
                    ProgramTotalTime = MessageCollection.Messages.Sum(x => x.DisplayTime.Minutes),
                };

       
           // MessageTableCollection.Clear();

            _navigationService.NavigateTo("MainPage", messageSet);
            
            //Messenger.Default.Send(messageSet);
            ProgramDescription = string.Empty;
            TableSetGuidId = Guid.Empty;
        }

        private void SetMessagesSortOrder()
        {
            MessageCollection.Messages.All(x => { x.SortOrder = MessageCollection.Messages.IndexOf(x)+1; return true; });
        }

        private RelayCommand<MessageTable> _editMessageTableCommand;
        public RelayCommand<MessageTable> EditMessageTableCommand => _editMessageTableCommand ?? (_editMessageTableCommand = new RelayCommand<MessageTable>(EditMessageTable, CanRemoveMessageTable));
        
      
        private void EditMessageTable(MessageTable messageToEdit)
        {
            _messageCollection.SelectedMessage = messageToEdit;
            _navigationService.NavigateTo("EditMessage");
        }


        private RelayCommand<MessageTable> _removeSelectedMessageTable;
        public RelayCommand<MessageTable> RemoveSelectedMessageTable => _removeSelectedMessageTable ?? (_removeSelectedMessageTable = new RelayCommand<MessageTable>(RemoveMessageTable,CanRemoveMessageTable));

        private bool CanRemoveMessageTable(MessageTable messageToRemove) => MessageCollection.Messages.Count != 0;


        private void RemoveMessageTable(MessageTable messageToRemove)
        {
           if(messageToRemove!=null)
            {
                int removeAtIndex = MessageCollection.Messages.IndexOf(messageToRemove);
                MessageCollection.Messages.Remove(messageToRemove);
                if(removeAtIndex!=0)
                {
                    MessageListSelectdIndex = removeAtIndex - 1;
                }
                else
                {
                    if(MessageCollection.Messages.Count!=0)
                    {
                        MessageListSelectdIndex = 0;
                    }
                }
            }
        }

   
        private bool CanMoveMessageUp(MessageTable messagetoMove) => MessageCollection.Messages.IndexOf(messagetoMove) != 0 && MessageCollection.Messages.Count!=0;

        private void MoveMessageUp(MessageTable messageToMove)
        {
            if (messageToMove != null)
            {
                int index = MessageCollection.Messages.IndexOf(messageToMove);

                if(index!=0)
                {
                    var previousMessage = MessageCollection.Messages[index - 1];
                    MessageCollection.Messages[index - 1] = messageToMove;
                    MessageCollection.Messages[index] = previousMessage;
                    MessageListSelectdIndex = MessageCollection.Messages.IndexOf(messageToMove);
                }
            }
        }
        
     
        private bool CanMoveMessageDown(MessageTable messageToMove) => MessageCollection.Messages.IndexOf(messageToMove) != MessageCollection.Messages.Count - 1;

        private void MoveMessageDown(MessageTable messageToMove)
        {
            if (messageToMove != null)
            {
                int index = MessageCollection.Messages.IndexOf(messageToMove);
               
                if (index != MessageCollection.Messages.Count-1)
                {
                    var previousMessage = MessageCollection.Messages[index + 1];
                    MessageCollection.Messages[index + 1] = messageToMove;
                    MessageCollection.Messages[index] = previousMessage;
                    MessageListSelectdIndex = MessageCollection.Messages.IndexOf(messageToMove);
                }
            }
        }


        /// <summary>
        /// Receive collection of messages, if MessageTableCollection collection already contains any of messages, update the message, if not- add it to the collection.
        /// </summary>
        private void GetMessages()
        {
            Messenger.Default.Register<ObservableCollection<MessageTable>>(
            this,
            messageCollection =>
            {
                foreach (MessageTable message in messageCollection)
                {
                    var savedMessage = MessageCollection.Messages.FirstOrDefault(x => x.GuidId == message.GuidId);

                    if (savedMessage != null)
                    {
                        savedMessage.MessageText = message.MessageText;
                        savedMessage.DisplayTime = message.DisplayTime;
                    }
                    else
                    {
                        MessageCollection.Messages.Add(message);
                    }
                }
            });
        }


        private void getMessageSet()
        {
           Messenger.Default.Register<MessageSetTable>(
           this,
             messageSet =>
             {
                 if (messageSet != null)
                 {
                     ProgramDescription = messageSet.Description;
                    // ProgramDifficulty =Convert.ToInt32(messageSet.ProgramDifficulty);
                     TableSetGuidId = messageSet.SetId;
                 }
             });
        }
    }
}