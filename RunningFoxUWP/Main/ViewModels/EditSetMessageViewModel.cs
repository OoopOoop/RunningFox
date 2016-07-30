using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
    public class EditSetMessageViewModel : ViewModelBase
    {
        public EditSetMessageViewModel(INavigationService navigationService)
        {
           base._navigationService = navigationService;
        }
    }
}
