using Main.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Main.ViewModels
{
    public class ProgramPlayer : ViewModelBase, IProgramPlayer
    {
        private DispatcherTimer _timer;
        private int _currentPlayingMessageIndex;

        private List<MessageSetTable> PlayCollection { get; set; }

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
        
        public void SetProgram(List<MessageSetTable> _playCollection)
        {
            PlayCollection = _playCollection;
            _currentPlayingMessageIndex = 0;
            SetMessageAndTime(_currentPlayingMessageIndex);
        }

    
        public void PlayProgram(MessageTable messageToPlay)
        {
            if(IsMessagePlaying())
            {
                StopProgram();
                return;
            }

            CurrentMessageTime = messageToPlay.DisplayTime;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        
        private void _timer_Tick(object sender, object e)
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

        }

        public void StopProgram()
        {
            _timer.Stop();
        }

        public string DisplayTime()
        {
            return null;
        }
    }
}
