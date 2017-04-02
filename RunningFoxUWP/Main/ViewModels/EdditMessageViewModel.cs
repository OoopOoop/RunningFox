using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
   public class EdditMessageViewModel:ViewModelBase
    {
        public EdditMessageViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;
        }
    }
}
