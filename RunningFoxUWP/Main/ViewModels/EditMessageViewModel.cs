using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
   public class EditMessageViewModel:ViewModelBase
    {

      

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(saveNewMessage));

        private void saveNewMessage()
        {

        }

        

        public EditMessageViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;


           
        }
    }
}
