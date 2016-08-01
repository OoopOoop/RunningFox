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

        private ObservableCollection<object> _testCollection;
        public ObservableCollection<object> TestCollection
        {
            get { return _testCollection; }
            set { _testCollection = value; OnPropertyChanged(); }
        }



        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(saveNewMessage));

        private void saveNewMessage()
        {

        }


     


        public EditMessageViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;


            TestCollection = new ObservableCollection<object>();
            
            TestCollection.Add("1");
            TestCollection.Add("2");
            TestCollection.Add("3");
            TestCollection.Add("4");
            TestCollection.Add("5");
            TestCollection.Add("6");
            TestCollection.Add("7");
            TestCollection.Add("8");
            TestCollection.Add("9");
            TestCollection.Add("10");
            TestCollection.Add("11");
            TestCollection.Add("12");
            TestCollection.Add("13");
            TestCollection.Add("14");
            TestCollection.Add("15");
            TestCollection.Add("16");
        }
    }
}
