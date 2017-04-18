using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Main.ViewModels
{
    //public class NamedColor
    //{
    //    public string Name { get; set; }
    //    public Color Color { get; set; }
    //}

    public class EditMessageViewModel : ViewModelBase
    {
        private TimeSpan _defaultTime=new TimeSpan(00, 05, 00);
        private string _defaultMessage=string.Empty;

        private RelayCommand _saveSingleMessageCommand;
        public RelayCommand SaveSingleMessageCommand => _saveSingleMessageCommand ?? (_saveSingleMessageCommand = new RelayCommand(saveNewMessages));

        private RelayCommand _populateMessagesCommand;
        public RelayCommand PopulateMessagesCommand => _populateMessagesCommand ?? (_populateMessagesCommand = new RelayCommand(populateMessage));

        private RelayCommand<object> _removeMessageFromList;
        public RelayCommand<object> RemoveMessageFromList => _removeMessageFromList ?? (_removeMessageFromList = new RelayCommand<object>(deleteMessage));

        private void deleteMessage(object message)
        {
            var messageTable = (MessageTable)message;
            if (messageTable != null)
            {
                PopulatedMessages.Remove(PopulatedMessages.Where(i => i.GuidID == messageTable.GuidID).FirstOrDefault());
            }

            ConfirmMessage = $"Message:  {messageTable.MessageText} was removed, duration: { messageTable.DisplayTimeText}";
        }

        private ObservableCollection<MessageTable> _populatedMessages;
        public ObservableCollection<MessageTable> PopulatedMessages
        {
            get { return _populatedMessages; }
            set { _populatedMessages = value; }
        }

        private void populateMessage()
        {
                var message = createNewMessage();
                PopulatedMessages.Add(message);

                //Clear time and messageToDisplay fields
                Time = _defaultTime;
                MessageToDisplay = _defaultMessage;
                NewMessage = new MessageTable();
                ConfirmMessage = $"Message:  {message.MessageText} was added, duration: { message.DisplayTimeText}";  
        }

        private string _messageToDisplay;
        public string MessageToDisplay
        {
            get { return _messageToDisplay; }
            set { _messageToDisplay = value; OnPropertyChanged(); }
        }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(); }
        }

        #region ColorPickers

        //private RelayCommand<NamedColor> _selectedForegroundColorCommand;
        //public RelayCommand<NamedColor> SelectedForegroundColorCommand => _selectedForegroundColorCommand ?? (_selectedForegroundColorCommand = new RelayCommand<NamedColor>(saveForegroundColor));

        //private void saveForegroundColor(NamedColor obj)
        //{
        //    var passedColor = obj as NamedColor;
        //    if (obj != null)
        //    {
        //        ForegroundColor = new SolidColorBrush(obj.Color);
        //    }

        //}

        //private SolidColorBrush _foregroundColor;
        //public SolidColorBrush ForegroundColor
        //{
        //    get { return _foregroundColor; }
        //    set { _foregroundColor = value; OnPropertyChanged(); }
        //}

        //private SolidColorBrush _backgroundColor;
        //public SolidColorBrush BackgroundColor
        //{
        //    get { return _backgroundColor; }
        //    set { _backgroundColor = value; OnPropertyChanged(); }
        //}

        //private RelayCommand<NamedColor> _selectedBackgroundColorCommand;
        //public RelayCommand<NamedColor> SelectedBackgroundColorCommand => _selectedBackgroundColorCommand ?? (_selectedBackgroundColorCommand = new RelayCommand<NamedColor>(saveBackgroundColor));

        //private void saveBackgroundColor(NamedColor obj)
        //{
        //    var passedColor = obj as NamedColor;
        //    if (obj != null)
        //    {
        //        BackgroundColor = new SolidColorBrush(obj.Color);
        //    }
        //}

        //public ObservableCollection<NamedColor> ColorsCollection { get; set; }

        //private void getColors()
        //{
        //    foreach (var color in typeof(Colors).GetRuntimeProperties())
        //    {
        //        ColorsCollection.Add(new NamedColor() { Name = color.Name, Color = (Color)color.GetValue(null) });
        //    }
        //}

        #endregion ColorPickers

        private MessageTable _newMessage;

        public MessageTable NewMessage
        {
            get { return _newMessage; }
            set { _newMessage = value; }
        }

        private MessageTable createNewMessage()
        {
            NewMessage.DisplayTime = this.Time;
            NewMessage.MessageText = string.IsNullOrEmpty(this.MessageToDisplay) ? "Run, Forrest, Run!" : MessageToDisplay;

            //check if message is new  than set new Guid, if it was sent to edit, save original guidID
            if (NewMessage.GuidID == null || NewMessage.GuidID == Guid.Empty)
            {
                NewMessage.GuidID = Guid.NewGuid();
            }
            return NewMessage;
        }

        private void saveNewMessages()
        {
            if (PopulatedMessages.Count == 0 || Time != _defaultTime || MessageToDisplay != _defaultMessage)
            {
                PopulatedMessages.Add(createNewMessage());
            }
            
            _navigationService.NavigateTo("EditSet");
            Messenger.Default.Send(PopulatedMessages);
        }

        private string _confirmMessage;

        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { _confirmMessage = value; OnPropertyChanged(); }
        }

        public EditMessageViewModel(INavigationService navigationService)
        {
            NewMessage = new MessageTable();

            // ColorsCollection = new ObservableCollection<NamedColor>();
            //getColors();
            PopulatedMessages = new ObservableCollection<MessageTable>();
            _navigationService = navigationService;
            Time = _defaultTime;
            getMessageToEdit();
        }

        public bool IsMessageToEdit { get; set; }

        private MessageTable getMessageToEdit()
        {
            Messenger.Default.Register<MessageTable>(
            this,
            message =>
            {
                MessageToDisplay = message.MessageText;
                Time = message.DisplayTime;
                NewMessage = message;
            });
            return NewMessage;
        }
    }
} //TODO: bind buttons enabled/disabled when deleting, edititing without selection, moving last item down or first item up.