using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Main.ViewModels
{
    //public class NamedColor
    //{
    //    public string Name { get; set; }
    //    public Color Color { get; set; }
    //}


    public class EditMessageViewModel:ViewModelBase
    {
        private RelayCommand _saveSingleMessageCommand;
        public RelayCommand SaveSingleMessageCommand => _saveSingleMessageCommand ?? (_saveSingleMessageCommand = new RelayCommand(saveNewMessages));


        private RelayCommand _populateMessagesCommand;
        public RelayCommand PopulateMessagesCommand => _populateMessagesCommand ?? (_populateMessagesCommand = new RelayCommand(savePopulatedMessages));


        private ObservableCollection<MessageTable> _populatedMessages;
        public ObservableCollection<MessageTable> PopulatedMessages
        {
            get { return _populatedMessages; }
            set { _populatedMessages = value; }
        }


        private void savePopulatedMessages()
        {
            var message = getMessage();

            PopulatedMessages.Add(message);
            Time = new TimeSpan(00, 05, 00);
            Message = string.Empty;


            ConfirmMessage = $"Message was added: {message.MessageText} for { message.DisplayTime} long ";
        }


        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
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

        #endregion

        private void saveNewMessages()
        {
                if(IsMessageToEdit)
                {
                    MessageToEdit.DisplayTime = this.Time;
                    MessageToEdit.MessageText = this.Message;
                    PopulatedMessages.Add(MessageToEdit);
                    IsMessageToEdit = false;
                }
                else
                {
                     //if(PopulatedMessages.Count!=0&&!string.IsNullOrEmpty(Message))             
                      PopulatedMessages.Add(getMessage());
                }
               
            _navigationService.NavigateTo("EditSet");
          
            Messenger.Default.Send(PopulatedMessages);
            PopulatedMessages = new ObservableCollection<MessageTable>();
        }



        private string _confirmMessage;
        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { _confirmMessage = value; OnPropertyChanged(); }
        }




        private MessageTable getMessage()
        {
            var message = new MessageTable()
            {
                DisplayTime = Time,
                MessageText = string.IsNullOrEmpty(this.Message)? "Run, Forrest, Run!":this.Message,
                MessageID = Guid.NewGuid(),
                SetID = Guid.NewGuid()
            };
            return message;
        }


        public EditMessageViewModel(INavigationService navigationService)
        {
            // ColorsCollection = new ObservableCollection<NamedColor>();
            PopulatedMessages = new ObservableCollection<MessageTable>();
            base._navigationService = navigationService;
            Time = new TimeSpan(00,05,00);
            //getColors();
            getMessageToEdit();
        }


        public bool IsMessageToEdit { get; set; }


        public MessageTable MessageToEdit { get; set; }

        
        private MessageTable getMessageToEdit()
        {
            Messenger.Default.Register<MessageTable>(
            this,
            message =>
            {
                this.Message = message.MessageText;
          
              // this.ForegroundColor = message.ColorForeground;
                this.Time = message.DisplayTime;
                IsMessageToEdit = true;
                MessageToEdit = message;
            });

            return MessageToEdit;
        }
        
    }
}
