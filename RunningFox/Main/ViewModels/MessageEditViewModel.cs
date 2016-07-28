using GalaSoft.MvvmLight.Command;
using Main.Models;
using Main.Shared;
using System;
using System.Windows.Media;

namespace Main.ViewModels
{
    public class MessageEditViewModel:NotifyService
    {
        private IFrameNavigationService _navigationService;
        

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            () =>
            {
                _navigationService.NavigateTo("EditMessageSet");
            }
            ));
        
        private RelayCommand _addNewMessageCommand;
        public RelayCommand AddNewMessageCommand => _addNewMessageCommand ?? (_addNewMessageCommand = new RelayCommand(
            () =>
            {
                var message = new MessageTable();
                message.MessageID = new Guid();
                message.MessageText = this.Description;
                message.ColorBackground = this.BackgroundColor;
                message.ColorForeground = this.ForegroundColor;
            }
            ));
        

        private int _selectedMin;
        public int SelectedMin
        {
            get { return _selectedMin; }
            set { _selectedMin = value; OnPropertyChanged(); }
        }


        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged();}
        }


        private SolidColorBrush _foregroundColor;
        public SolidColorBrush ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; OnPropertyChanged(); }
        }


        private SolidColorBrush _backgroundColor;
        public SolidColorBrush BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(); }
        }

        
     
       



        public MessageEditViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;

            SelectedMin = 3;


            ForegroundColor = new SolidColorBrush(Colors.Yellow);

            //  BackgroundColor = Colors.White;


            Color color = ForegroundColor.Color;

          
        }
    }
}
