using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
   public class MainViewModel:ViewModelBase
    {
        private ObservableCollection<MessageSetTable> _messageSetCollection;
        public ObservableCollection<MessageSetTable> MessageSetCollection
        {
            get { return _messageSetCollection; }
            set { _messageSetCollection = value; OnPropertyChanged(); }
        }


        public MainViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;
            MessageSetCollection = new ObservableCollection<MessageSetTable>();
            getMessageSets();
        }



        //ToRemove
        private string _testDescription;
        public string TestDescription
        {
            get { return _testDescription; }
            set { _testDescription = value; OnPropertyChanged(); }
        }

        

        private void getMessageSets()
        {
            Messenger.Default.Register<MessageSetTable>(
           this,
           messageSet =>
           {

               TestDescription = messageSet.Description;

               MessageSetCollection.Add(messageSet);
           });
        }
    }
}
