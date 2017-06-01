using System;
using System.Linq;
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
        
        private int currentMessageIndex=0;

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
            currentMessageIndex++;
            setNewMessage(currentMessageIndex);
        }

        private bool CanSetProgram() => PlayCollection.MessageCollection.IndexOf(PlayCollection.MessageCollection[currentMessageIndex])!= -1;
        
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
            if (PlayCollection.MessageCollection.ElementAtOrDefault(currentMessageIndex) != null)
            {
                var time = PlayCollection.MessageCollection[currentMessageIndex].DisplayTime;

                _basetime = new TimeSpan(0, time.Hours, time.Minutes, time.Seconds);

                TimeLeft = _basetime.ToString();
                _timer.Interval = new TimeSpan(0, 0, 1);

                _timer.Tick += _timer_Tick;
                _timer.Start();

                CanSetProgram();
            }
        }

        private void _timer_Tick(object sender, object e)
        {  
            _basetime = _basetime.Subtract(TimeSpan.FromSeconds(1));
            TimeLeft = _basetime.ToString();
            if (_basetime == new TimeSpan(0, 0, 0, 0))
            {
                _timer.Stop();
                 currentMessageIndex++;
                 setNewMessage(currentMessageIndex);
               
                //checkForLastMessage(currentMessageIndex);
            }
            CanSetProgram();
        }
        
        //private void checkForLastMessage(int index)
        //{
        //    if (PlayCollection.MessageCollection.Count == index)
        //    {
        //        CanSetProgram();
        //    }
        //}

        private void setNewMessage(int index)
        {
            if (PlayCollection.MessageCollection.ElementAtOrDefault(index) != null)
            {
                CurrentMessage = PlayCollection?.MessageCollection[index].MessageText;
                StartProgram();
            }
            else
            {
                _timer.Stop();
            }
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
