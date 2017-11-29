using GalaSoft.MvvmLight.Command;
using Main.Models;
using System;
using Windows.UI.Xaml;

namespace Main.ViewModels
{
    public class ProgramPlayer : ViewModelBase, IProgramPlayer
    {
        private const string STARTBUTTONCONTENT = "Start";
        private const string PAUSEBUTTONCONTENT = "Pause";

        private bool _repeatButtonVisibility;
        public bool RepeatButtonVisibility
        {
            get { return _repeatButtonVisibility; }
            set { _repeatButtonVisibility = value; OnPropertyChanged(); }
        }

        private TimeSpan _totalExercisesTime;
        public TimeSpan TotalExercisesTime
        {
            get { return _totalExercisesTime; }
            set { _totalExercisesTime = value; OnPropertyChanged(); }
        }

        private DispatcherTimer _timer;
        private int _currentPlayingMessageIndex;

        private MessageSetTable PlayCollection { get; set; }

        private TimeSpan _currentMessageTime;
        public TimeSpan CurrentMessageTime
        {
            get { return _currentMessageTime; }
            set { _currentMessageTime = value; OnPropertyChanged(); }
        }

        private TimeSpan _previousMessageTime;
        public TimeSpan PreviousMessageTime
        {
            get { return _previousMessageTime; }
            set { _previousMessageTime = value; OnPropertyChanged(); }
        }

        private TimeSpan _nextMessageTime;
        public TimeSpan NextMessageTime
        {
            get { return _nextMessageTime; }
            set { _nextMessageTime = value; OnPropertyChanged(); }
        }

        private TimeSpan _totalExerciseTime;
        public TimeSpan TotalExerciseTime
        {
            get { return _totalExerciseTime; }
            set { _totalExerciseTime = value; OnPropertyChanged(); }
        }

        private string _currentMessage;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set { _currentMessage = value; OnPropertyChanged(); }
        }
        
        private string _previousMessage;
        public string PreviousMessage
        {
            get { return _previousMessage; }
            set { _previousMessage = value; OnPropertyChanged(); }
        }
        
        private string _nextMessage;
        public string NextMessage
        {
            get { return _nextMessage; }
            set { _nextMessage = value; OnPropertyChanged(); }
        }

        private string _playButtonContext;
        public string PlayButtonContext
        {
            get { return _playButtonContext; }
            set { _playButtonContext = value; OnPropertyChanged(); }
        }

        public  bool IsMessagePlaying()
        {
            return _timer.IsEnabled;
        }

        private RelayCommand _setNextProgrammCommand;
        public RelayCommand SetNextProgrammCommand => _setNextProgrammCommand ?? (_setNextProgrammCommand = new RelayCommand(SetNextProgram));

        private RelayCommand _repeatProgramCommand;
        public RelayCommand RepeatProgramCommand => _repeatProgramCommand ?? (_repeatProgramCommand = new RelayCommand(RepeatProgram));

        private RelayCommand _startProgrammCommand;
        public RelayCommand StartProgrammCommand => _startProgrammCommand ?? (_startProgrammCommand = new RelayCommand(PlayProgram));

        private void SetNextProgram()
        {
            _timer.Tick -= Timer_Tick;
            _currentPlayingMessageIndex++;
            SetMessageAndTime(_currentPlayingMessageIndex);
        }

        public void SetProgram(MessageSetTable playCollection)
        {
            PlayCollection = playCollection;
            _currentPlayingMessageIndex = 0;
            SetMessageAndTime(_currentPlayingMessageIndex);
        }
        
        public void PlayProgram()
        {
            if(IsMessagePlaying())
            {
                StopProgram();
                return;
            }

            CurrentMessageTime =PlayCollection.MessageCollection[_currentPlayingMessageIndex].DisplayTime;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        
        private void Timer_Tick(object sender, object e)
        {
            TotalExerciseTime = TotalExerciseTime.Add(TimeSpan.FromSeconds(1));
            if(CurrentMessageTime!=TimeSpan.FromSeconds(0))
            {
                CurrentMessageTime = CurrentMessageTime.Subtract(TimeSpan.FromSeconds(1));
                return;
            }

            _timer.Stop();
            SetMessageAndTime(_currentPlayingMessageIndex);
        }
        
        private void SetMessageAndTime(int currentIndex)
        {
            if (PlayCollection.MessageCollection[currentIndex] == null)
            {
                if (PlayCollection.SetToRepeat)
                {
                    RepeatProgram();
                }
                return;
            }

            //set previous message and time
            PreviousMessage = PlayCollection.MessageCollection[currentIndex - 1]!=null?PlayCollection.MessageCollection[currentIndex-1].MessageText:String.Empty;
            PreviousMessageTime = PlayCollection.MessageCollection[currentIndex - 1] != null ? PlayCollection.MessageCollection[currentIndex - 1].DisplayTime : new TimeSpan(0,0,0);

            //set next message and time
            NextMessage = PlayCollection.MessageCollection[currentIndex +1] != null ? PlayCollection.MessageCollection[currentIndex + 1].MessageText : String.Empty;
            NextMessageTime = PlayCollection.MessageCollection[currentIndex + 1].DisplayTime;
        }

        public void StopProgram()
        {
            _timer.Stop();
        }
        
        private void RepeatProgram()
        {
            SetMessageAndTime(0);
        }

        public void PlayProgram(MessageTable messageToPlay)
        {
            throw new NotImplementedException();
        }
    }
}
