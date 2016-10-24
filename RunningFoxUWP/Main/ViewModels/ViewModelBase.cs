using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));


        private RelayCommand<string> _navigateToCommand;
        public RelayCommand<string> NavigateToCommand => _navigateToCommand ?? (_navigateToCommand = new RelayCommand<string>(
            (string commandParameter) =>
            {
                _navigationService.NavigateTo(commandParameter);
            }));
    }
}
