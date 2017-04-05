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


        private int _messagesTotalCount;
        public int MessagesTotalCount
        {
            get { return _messagesTotalCount; }
            set { _messagesTotalCount = value; OnPropertyChanged(); }
        }


        private int _programTotalTime;
        public int ProgramTotalTime
        {
            get { return _programTotalTime; }
            set { _programTotalTime = value; OnPropertyChanged(); }
        }


        private string _programDifficulty;
        public string ProgramDifficulty
        {
            get { return _programDifficulty; }
            set { _programDifficulty = value;OnPropertyChanged(); }
        }
        

        public MainViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;
            MessageSetCollection = new ObservableCollection<MessageSetTable>();
            getMessageSets();
        }


        private void getMessageSets()
        {
            Messenger.Default.Register<MessageSetTable>(
           this,
           messageSet =>
           {
               MessageSetCollection.Add(messageSet);
           });
        }
    }
}
