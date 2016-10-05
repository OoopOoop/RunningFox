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
    public class NamedColor
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }


    public class EditMessageViewModel:ViewModelBase
    {
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(saveNewMessage));

        private RelayCommand<NamedColor> _selectedForegroundColorCommand;
        public RelayCommand<NamedColor> SelectedForegroundColorCommand => _selectedForegroundColorCommand ?? (_selectedForegroundColorCommand = new RelayCommand<NamedColor>(saveForegroundColor));

        private void saveForegroundColor(NamedColor obj)
        {
            var passedColor = obj as NamedColor;
            if (obj != null)
            {
                ForegroundColor = new SolidColorBrush(obj.Color);
            }

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

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }


        private RelayCommand<NamedColor> _selectedBackgroundColorCommand;
        public RelayCommand<NamedColor> SelectedBackgroundColorCommand => _selectedBackgroundColorCommand ?? (_selectedBackgroundColorCommand = new RelayCommand<NamedColor>(saveBackgroundColor));


        private void saveBackgroundColor(NamedColor obj)
        {
            var passedColor = obj as NamedColor;
            if (obj != null)
            {
                BackgroundColor = new SolidColorBrush(obj.Color);
            }
        }


        public ObservableCollection<NamedColor> ColorsCollection { get; set; }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value;OnPropertyChanged(); }
        }

        private void getColors()
        {
            foreach (var color in typeof(Colors).GetRuntimeProperties())
            {
                ColorsCollection.Add(new NamedColor() { Name = color.Name, Color = (Color)color.GetValue(null) });
            }
        }
    

        private void saveNewMessage()
        {
            var message = new MessageTable()
            { DisplayTime = Time,
                ColorForeground = this.ForegroundColor ?? new SolidColorBrush(Colors.Black),
                ColorBackground = this.BackgroundColor ?? new SolidColorBrush(Colors.White),
                MessageText = this.Message?? "Run, Forrest, Run!",
                MessageID =Guid.NewGuid(),
                SetID =Guid.NewGuid()};
           
            _navigationService.NavigateTo("EditSet");
            Messenger.Default.Send(message);
        }
        


        public EditMessageViewModel(INavigationService navigationService)
        {
            ColorsCollection = new ObservableCollection<NamedColor>();
            base._navigationService = navigationService;
            Time = new TimeSpan(00,05,00);
            getColors();
        }
    }
}
