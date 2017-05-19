using System;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private TimeSpan _basetime;


        //private int currentMessageIndex=0;

        private MessageSetTable _playCollection;
        public  MessageSetTable PlayCollection
        {
            get { return _playCollection; }
            set { _playCollection = value; OnPropertyChanged(); }
        }


        private RelayCommand _setNextProgrammCommand;
        public RelayCommand SetNextProgrammCommand => _setNextProgrammCommand ?? (_setNextProgrammCommand = new RelayCommand(SetNextProgram, CanSetProgram));

        private void SetNextProgram()
        {
            
        }

        private bool CanSetProgram() => PlayCollection.MessageCollection.Count != 0;



        private RelayCommand _pauseProgrammCommand;
        public RelayCommand PauseProgrammCommand => _pauseProgrammCommand ?? (_pauseProgrammCommand = new RelayCommand(PauseProgram));

        private void PauseProgram()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }


        private RelayCommand _startProgrammCommand;
        public RelayCommand StartProgrammCommand => _startProgrammCommand ?? (_startProgrammCommand = new RelayCommand(StartProgram, CanStartProgram));
        
        private bool CanStartProgram() => !_timer.IsEnabled;


        private void StartProgram()
        {
            //var time = PlayCollection.MessageCollection[currentMessageIndex].DisplayTime;

            //TODO: check for null; create a playlist with previous message, and next one

            var time = PlayCollection.MessageCollection[0].DisplayTime;

            _basetime = new TimeSpan(0,time.Hours, time.Minutes, time.Seconds);

            TimeLeft = _basetime.ToString();
            _timer.Interval = new TimeSpan(0, 0, 1);

            _timer.Tick += _timer_Tick;
            _timer.Start();         

            PlayCollection.MessageCollection.RemoveAt(0);
        }

        private void _timer_Tick(object sender, object e)
        {  
            _basetime = _basetime.Subtract(TimeSpan.FromSeconds(1));
            TimeLeft = _basetime.ToString();
            if (_basetime == new TimeSpan(0, 0, 0, 0))
            {
                _timer.Stop();
                //  currentMessageIndex++;
                //setNewMessage(currentMessageIndex);

                setNewMessage();
            }
        }

        //private void setNewMessage(int index)
        //{
        //    CurrentMessage = PlayCollection?.MessageCollection[index].MessageText;
        //    StartProgram();
        //}


        private void setNewMessage()
        {
            CurrentMessage = PlayCollection?.MessageCollection[0].MessageText;
            StartProgram();
        }

        public PlayPageViewModel(INavigationService navigationService)
        {
            base._navigationService=navigationService;
            getProgramToPlay();
            _timer = new DispatcherTimer();
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
