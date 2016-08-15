using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

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
            throw new NotImplementedException();
        }

    

        private RelayCommand<NamedColor> _selectedBackgroundColorCommand;
        public RelayCommand<NamedColor> SelectedBackgroundColorCommand => _selectedBackgroundColorCommand ?? (_selectedBackgroundColorCommand = new RelayCommand<NamedColor>(saveBackgroundColor));

        private void saveBackgroundColor(NamedColor obj)
        {
            throw new NotImplementedException();
        }

      
        public ObservableCollection<NamedColor> Colors { get; set; }

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
                Colors.Add(new NamedColor() { Name = color.Name, Color = (Color)color.GetValue(null) });
            }
        }
    
        private void saveNewMessage()
        {
           
        }
        


        public EditMessageViewModel(INavigationService navigationService)
        {
            Colors = new ObservableCollection<NamedColor>();

            base._navigationService = navigationService;
            Time = new TimeSpan(0, 5,0);
            getColors();
        }
    }
}
