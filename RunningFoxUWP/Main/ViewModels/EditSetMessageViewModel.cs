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
        private bool _isSetToRepeat;
        public bool IsSetToRepeat
        {
            get { return _isSetToRepeat; }
            set { _isSetToRepeat = value; OnPropertyChanged(); }
        }


        private int _messageListSelectdIndex=0;
        public int MessageListSelectdIndex
        {
            get { return _messageListSelectdIndex; }
            set { _messageListSelectdIndex = value;OnPropertyChanged(); }
        }
        
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
            getMessageSet();          
        }


        private string _programDescription;
        public string ProgramDescription
        {
            get { return _programDescription; }
            set { _programDescription = value; OnPropertyChanged(); }
        }

        
        private RelayCommand _saveNewProgramCommand;
        public RelayCommand SaveNewProgramCommand => _saveNewProgramCommand ?? (_saveNewProgramCommand = new RelayCommand(saveNewSet));


        //private RelayCommand<string> _diffucultyIsCheckedCommand;
        //public RelayCommand<string> DiffucultyIsCheckedCommand =>_diffucultyIsCheckedCommand ?? (_diffucultyIsCheckedCommand = new RelayCommand<string>(setDifficulty));

        private Guid TableSetGuidId;

        private string programDiffculty;
       
        //private bool valueAsEasy;
        //public bool ValueAsEasy
        //{
        //    get { return Value.Equals(1); }
        //    set { Value = 1; OnPropertyChanged(nameof(Value)); }
        //}

        //private bool valueAsMedium;
        //public bool ValueAsMedium
        //{
        //    get { return Value.Equals(2); }
        //    set { Value = 2; OnPropertyChanged(nameof(Value)); }
        //}

        //private bool valueAsHard;
        //public bool ValueAsHard
        //{
        //    get { return Value.Equals(3); }
        //    set { Value = 3;
        //        OnPropertyChanged(nameof(Value));
        //    }
        //}

        //private int _value=default(int);
        //public int Value
        //{
        //    get { return _value; }
        //    set
        //    {
        //        _value = value;
                
        //        //OnPropertyChanged(nameof(ValueAsEasy));
        //        //OnPropertyChanged(nameof(ValueAsMedium));
        //        //OnPropertyChanged(nameof(ValueAsHard));
        //    }
        //}

        
        //private void setDifficulty(string diffuculty)
        //{
        //    programDiffculty = diffuculty;
        //}

        private void saveNewSet()
        {
            SetMessagesSortOrder();

                var messageSet = new MessageSetTable()
                {
                    Description = ProgramDescription,
                    MessageCollection = MessageTableCollection,
                    SetID = TableSetGuidId == Guid.Empty?Guid.NewGuid(): TableSetGuidId,
                    SetToRepeat = IsRepeating,                 
                   // ProgramDifficulty = Value.ToString(),
                    MessagesTotalCount = MessageTableCollection.Count,
                    ProgramTotalTime = MessageTableCollection.Sum(x => x.DisplayTime.Minutes),
                };

            Messenger.Default.Send(messageSet);
            
          //  MessageTableCollection.Clear();

            _navigationService.NavigateTo("MainPage");
            ProgramDescription = string.Empty;
            TableSetGuidId = Guid.Empty;

        }

        private void SetMessagesSortOrder()
        {
            MessageTableCollection.All(x => { x.SortOrder = MessageTableCollection.IndexOf(x)+1; return true; });
        }

        private RelayCommand<MessageTable> _editMessageTableCommand;
        public RelayCommand<MessageTable> EditMessageTableCommand => _editMessageTableCommand ?? (_editMessageTableCommand = new RelayCommand<MessageTable>(EditMessageTable, canRemoveMessageTable));
        
      
        private void EditMessageTable(MessageTable messageToEdit)
        {
            _navigationService.NavigateTo("EditMessage");
            Messenger.Default.Send(messageToEdit);
        }


        private RelayCommand<MessageTable> _removeSelectedMessageTable;
        public RelayCommand<MessageTable> RemoveSelectedMessageTable => _removeSelectedMessageTable ?? (_removeSelectedMessageTable = new RelayCommand<MessageTable>(removeMessageTable,canRemoveMessageTable));

        private bool canRemoveMessageTable(MessageTable messageToRemove) => MessageTableCollection.Count != 0;


        private void removeMessageTable(MessageTable messageToRemove)
        {
           if(messageToRemove!=null)
            {
                int removeAtIndex = MessageTableCollection.IndexOf(messageToRemove);
                MessageTableCollection.Remove(messageToRemove);
                if(removeAtIndex!=0)
                {
                    MessageListSelectdIndex = removeAtIndex - 1;
                }
                else
                {
                    if(MessageTableCollection.Count!=0)
                    {
                        MessageListSelectdIndex = 0;
                    }
                }
            }
        }

        private RelayCommand<MessageTable> _moveUpMessageTableCommand;
        public RelayCommand<MessageTable> MoveUpMessageTableCommand => _moveUpMessageTableCommand ?? (_moveUpMessageTableCommand = new RelayCommand<MessageTable>(MoveMessageUp, CanMoveMessageUp));
        

        private bool CanMoveMessageUp(MessageTable messagetoMove) => MessageTableCollection.IndexOf(messagetoMove) != 0&&MessageTableCollection.Count!=0;

        private void MoveMessageUp(MessageTable messageToMove)
        {
            if (messageToMove != null)
            {
                int index = MessageTableCollection.IndexOf(messageToMove);

                if(index!=0)
                {
                    var previousMessage = MessageTableCollection[index - 1];
                    MessageTableCollection[index - 1] = messageToMove;
                    MessageTableCollection[index] = previousMessage;
                    MessageListSelectdIndex = MessageTableCollection.IndexOf(messageToMove);
                }
            }
        }
        
        //TODO: make all methods and properties uppercases

        private RelayCommand<MessageTable> _moveDownMessageTableCommand;
        public RelayCommand<MessageTable> MoveDownMessageTableCommand => _moveDownMessageTableCommand ?? (_moveDownMessageTableCommand = new RelayCommand<MessageTable>(moveMessageDown, canMoveMessageDown));

        private bool canMoveMessageDown(MessageTable messageToMove) => MessageTableCollection.IndexOf(messageToMove) != MessageTableCollection.Count - 1;

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
                    MessageListSelectdIndex = MessageTableCollection.IndexOf(messageToMove);
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
                    var savedMessage = MessageTableCollection.Where(x => x.GuidID == message.GuidID).FirstOrDefault();

                    if (savedMessage != null)
                    {
                        savedMessage.MessageText = message.MessageText;
                        savedMessage.DisplayTime = message.DisplayTime;
                    }
                    else
                    {
                        MessageTableCollection.Add(message);
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
                     programDiffculty = messageSet.ProgramDifficulty;
                     TableSetGuidId = messageSet.SetID;
                 }
             });
        }
    }
}