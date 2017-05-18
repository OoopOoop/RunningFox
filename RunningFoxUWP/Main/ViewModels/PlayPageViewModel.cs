using System;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using GalaSoft.MvvmLight.Command;

namespace Main.ViewModels
{
    public class PlayPageViewModel: ViewModelBase
    {
        private DispatcherTimer _timer;
        private int _basetime;
        
        private MessageSetTable _playCollection;
        public  MessageSetTable PlayCollection
        {
            get { return _playCollection; }
            set { _playCollection = value; OnPropertyChanged(); }
        }
        
        private RelayCommand _startProgrammCommand;
        public RelayCommand StartProgrammCommand => _startProgrammCommand ?? (_startProgrammCommand = new RelayCommand(StartProgram));
        
        private void StartProgram()
        {
            _basetime = PlayCollection.MessageCollection[0].DisplayTime.Minutes;

            TimeLeft = _basetime.ToString();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0,0,0,1);
            _timer.Tick += _timer_Tick;
            
            _timer.Start();
        }

        private void _timer_Tick(object sender, object e)
        {
            _basetime = _basetime - 1;
            TimeLeft = _basetime.ToString();
            if (_basetime == 0)
            {
                _timer.Stop();
            }
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


        private string _timeLeft;
        public string TimeLeft
        {
            get { return _timeLeft; }
            set { _timeLeft = value; OnPropertyChanged(); }
        }

        private void getProgramToPlay()
        {
            Messenger.Default.Register<MessageSetTable>(
                this, messageSet =>
                    {
                        PlayCollection = messageSet;
                        CurrentMessage = PlayCollection?.MessageCollection[0].MessageText;
                        TimeLeft = messageSet.MessageCollection[0].DisplayTime.ToString();
                    });
        }
    }
}
