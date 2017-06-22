using System;
using System.Linq;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace Main.ViewModels
{
    public class PlayPageViewModel: ViewModelBase
    {
        private DispatcherTimer _timer;
        private TimeSpan _basetime;

        private TimeSpan _totalTimeLeft;
        public TimeSpan TotalTimeLeft
        {
            get { return _totalTimeLeft; }
            set { _totalTimeLeft = value;OnPropertyChanged();}
        }
        

        private TimeSpan _totalExercisesTime;
        public TimeSpan TotalExercisesTime
        {
            get { return _totalExercisesTime; }
            set { _totalExercisesTime = value; OnPropertyChanged(); }
        }


        private int currentMessageIndex=0;
        
        //previous
        private string _previousMessage;
        public string PreviousMessage
        {
            get { return _previousMessage; }
            set { _previousMessage = value; OnPropertyChanged(); }
        }

        private string _previousMesageTimeLeft;
        public string PreviousMesageTimeLeft
        {
            get { return _previousMesageTimeLeft; }
            set { _previousMesageTimeLeft = value; OnPropertyChanged(); }
        }

        //current
        private string _currentMessage;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set { _currentMessage = value; OnPropertyChanged(); }
        }

        private string _currentTimeLeft;
        public string CurrentTimeLeft
        {
            get { return _currentTimeLeft; }
            set { _currentTimeLeft = value; OnPropertyChanged(); }
        }

        //next
        private string _nextMessage;
        public string NextMessage
        {
            get { return _nextMessage; }
            set { _nextMessage = value; OnPropertyChanged(); }
        }
        private string _nextMessageTimeLeft;
        public string NextMessageTimeLeft
        {
            get { return _nextMessageTimeLeft; }
            set { _nextMessageTimeLeft = value; OnPropertyChanged(); }
        }

      
        private MessageSetTable _playCollection;
        public  MessageSetTable PlayCollection
        {
            get { return _playCollection; }
            set { _playCollection = value; OnPropertyChanged(); }
        }
        
        private RelayCommand _setNextProgrammCommand;
        public RelayCommand SetNextProgrammCommand => _setNextProgrammCommand ?? (_setNextProgrammCommand = new RelayCommand(SetNextProgram));

        private void SetNextProgram()
        {
            currentMessageIndex++;
            setNewMessage(currentMessageIndex);
        }

     
        private RelayCommand _pauseProgrammCommand;
        public RelayCommand PauseProgrammCommand => _pauseProgrammCommand ?? (_pauseProgrammCommand = new RelayCommand(PauseProgram));

        private void PauseProgram()
        {
            if (!_timer.IsEnabled)
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
                TotalExercisesTime = new TimeSpan(0, 0, 0, 0);


                //TODO: update total time when next message is clicked
                // TotalTimeSpan.Subtract(PlayCollection.MessageCollection[currentMessageIndex - 1].DisplayTime);


                CurrentTimeLeft = _basetime.ToString();
                _timer.Interval = new TimeSpan(0, 0, 1);

                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        private void _timer_Tick(object sender, object e)
        {  
            _basetime = _basetime.Subtract(TimeSpan.FromSeconds(1));
            TotalTimeLeft = TotalTimeLeft.Subtract(TimeSpan.FromSeconds(1));
         
            CurrentTimeLeft = _basetime.ToString();

            TotalExercisesTime = TotalExercisesTime.Add(TimeSpan.FromSeconds(1));


            if (_basetime == new TimeSpan(0, 0, 0, 0))
            {
                _timer.Stop();
                 currentMessageIndex++;
                 setNewMessage(currentMessageIndex);
            }
        }
        
     
        private void setNewMessage(int index)
        {
            if (PlayCollection.MessageCollection.ElementAtOrDefault(index) != null)
            {
                _setDisplayMessagesAndDuration(index);
                StartProgram();
            }
            else
            {
                _timer.Stop();
            }
        }

        public PlayPageViewModel(INavigationService navigationService)
        {
            PlayCollection = new MessageSetTable
            {
                MessagesTotalCount = 4,
                SetToRepeat = false,
                MessageCollection = new ObservableCollection<MessageTable>()
                {new MessageTable(){ SortOrder=1, DisplayTime = new TimeSpan(0,2,0), MessageText="test message 1"},
                 new MessageTable(){ SortOrder=2, DisplayTime = new TimeSpan(0,1,0), MessageText="test message 2"},
                 new MessageTable(){ SortOrder=3, DisplayTime = new TimeSpan(0,5,0), MessageText="test message 3"},
                 new MessageTable(){ SortOrder=4, DisplayTime = new TimeSpan(0,2,0), MessageText="test message 4"},
                 new MessageTable(){ SortOrder=5, DisplayTime = new TimeSpan(0,1,0), MessageText="test message 5"},
                 new MessageTable(){ SortOrder=6, DisplayTime = new TimeSpan(0,5,0), MessageText="test message 6"}
                }
            };

            TotalTimeLeft = getTotalTimeLeft();

            _setDisplayMessagesAndDuration(0);
          
            base._navigationService=navigationService;
            _timer = new DispatcherTimer();
            // getProgramToPlay();

        }

        private TimeSpan getTotalTimeLeft()
        {
            var time = new TimeSpan();

            foreach (var item in PlayCollection.MessageCollection)
            {
                time = time.Add(item.DisplayTime);
            }
            return time;
        }


        private void _setDisplayMessagesAndDuration(int currentMessageIndex)
        {
            bool isLastMessageActive = currentMessageIndex + 1 == PlayCollection.MessageCollection.Count;
            bool isFirstMessageActive = currentMessageIndex == 0;
            bool isOnRepeat = PlayCollection.SetToRepeat;

            CurrentMessage = PlayCollection?.MessageCollection[currentMessageIndex].MessageText;
            CurrentTimeLeft = PlayCollection.MessageCollection[currentMessageIndex].DisplayTime.ToString();


            if (!isFirstMessageActive)
            {
                PreviousMessage = PlayCollection?.MessageCollection[currentMessageIndex - 1].MessageText;
                PreviousMesageTimeLeft = PlayCollection.MessageCollection[currentMessageIndex - 1].DisplayTime.ToString();
            }

            if (isLastMessageActive)
            {
                if(isOnRepeat)
                {
                    repeatProgram();
                }
                
            }
         else
            {
                NextMessage = PlayCollection.MessageCollection[currentMessageIndex + 1].MessageText;
                NextMessageTimeLeft = PlayCollection.MessageCollection[currentMessageIndex + 1].DisplayTime.ToString();               
            }
        }

        private void repeatProgram()
        {
            currentMessageIndex = 0;
            StartProgram();
        }



        private void getProgramToPlay()
        {
            Messenger.Default.Register<MessageSetTable>(
                this, messageSet =>
                    {
                        PlayCollection = messageSet;
                        CurrentMessage = PlayCollection?.MessageCollection[0].MessageText;
                        CurrentTimeLeft = messageSet.MessageCollection[0].DisplayTime.ToString();
                    });
        }
    }
}
