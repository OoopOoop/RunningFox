using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;

namespace Main.ViewModels
{
    public class PlayPageViewModel: ViewModelBase
    {
        private ObservableCollection<MessageSetTable> _playCollection;
        public ObservableCollection<MessageSetTable> PlayCollection
        {
            get { return _playCollection; }
            set { _playCollection = value; OnPropertyChanged(); }
        }


        public PlayPageViewModel(INavigationService navigationService)
        {
            base._navigationService=navigationService;
            getProgramToPlay();
        }

        private string _currentMessage;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set { _currentMessage = value; OnPropertyChanged(); }
        }


        private string _nextMessage;
        public string NextMessage
        {
            get { return _nextMessage; }
            set { _nextMessage = value; OnPropertyChanged(); }
        }


        private DateTime _timeLeft;
        public DateTime TimeLeft
        {
            get { return _timeLeft; }
            set { _timeLeft = value; OnPropertyChanged(); }
        }

        private void getProgramToPlay()
        {
            Messenger.Default.Register<MessageSetTable>(
                this,
                messageSet => { CurrentMessage = messageSet.MessageCollection[0].MessageText; });
        }
    }
}
