using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool _isProgramInUse;

        private const string STARTBUTTONCONTENT = "Start";
        private const string PAUSEBUTTONCONTENT = "Pause";

        private TimeSpan _messageTime;
        public TimeSpan MessageTime
        {
            get { return _messageTime; }
            set
            {
                _messageTime = value; OnPropertyChanged(); }
        }
        
        private TimeSpan _totalExercisesTime;
        public TimeSpan TotalExercisesTime
        {
            get { return _totalExercisesTime; }
            set { _totalExercisesTime = value; OnPropertyChanged(); }
        }


        private string _startPauseBtnText;
        public string StartPauseBtnText
        {
            get { return _startPauseBtnText; }
            set { _startPauseBtnText = value; OnPropertyChanged(); }
        }
        

        private bool _repeatButtonVisibility;
        public bool RepeatButtonVisibility
        {
            get { return _repeatButtonVisibility; }
            set { _repeatButtonVisibility = value; OnPropertyChanged(); }
        }


        private int currentMessageIndex=0;
        
        //previous
        private string _previousMessage;
        public string PreviousMessage
        {
            get { return _previousMessage; }
            set
            {
                _previousMessage = value; OnPropertyChanged(); }
        }

        private string _previousMesageTimeLeft;
        public string PreviousMesageTimeLeft
        {
            get { return _previousMesageTimeLeft; }
            set { _previousMesageTimeLeft =value; OnPropertyChanged(); }
        }

        //current
        private string _currentMessage;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                _currentMessage = value; OnPropertyChanged(); }
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
            _timer.Tick -= _timer_Tick;
            currentMessageIndex++;
            SetNewMessage(currentMessageIndex);
        }
        

        private void _pauseProgram()
        {
            StartPauseBtnText = STARTBUTTONCONTENT;
            _timer.Stop();
            _isProgramInUse = true;
        }

        private RelayCommand _repeatProgramCommand;
        public RelayCommand RepeatProgramCommand => _repeatProgramCommand ?? (_repeatProgramCommand = new RelayCommand(RepeatProgram));


        private RelayCommand _startProgrammCommand;
        public RelayCommand StartProgrammCommand => _startProgrammCommand ?? (_startProgrammCommand = new RelayCommand(StartProgram));

     
        private void StartProgram()
        {
            if (_timer.IsEnabled)
            {
                _pauseProgram();
               
            }
            else
            {
                _startProgram();
            }
        }
        

        private void _startProgram()
        {
            StartPauseBtnText = PAUSEBUTTONCONTENT;

            if (_isProgramInUse)
            {
                _timer.Start();
            }
            else
            {
                _startNewMessageProgram();
            }
        }
        

        private void _startNewMessageProgram()
        {
            if (PlayCollection.MessageCollection.ElementAtOrDefault(currentMessageIndex) != null)
            {
                StartPauseBtnText = PAUSEBUTTONCONTENT;

                var time = PlayCollection.MessageCollection[currentMessageIndex].DisplayTime;

                MessageTime = new TimeSpan(0, time.Hours, time.Minutes, time.Seconds);

                _timer.Interval = new TimeSpan(0, 0, 1);

                _timer.Tick += _timer_Tick;
                
                _timer.Start();              
            }
        }

        
        private void _timer_Tick(object sender, object e)
        {
            MessageTime = MessageTime.Subtract(TimeSpan.FromSeconds(1));
          
            TotalExercisesTime = TotalExercisesTime.Add(TimeSpan.FromSeconds(1));


            if (MessageTime == new TimeSpan(0, 0, 0, 0))
            {
                _timer.Stop();
                 currentMessageIndex++;
                 SetNewMessage(currentMessageIndex);
            }
        }
        
     
        private void SetNewMessage(int index)
        {
            if (PlayCollection.MessageCollection.ElementAtOrDefault(index) != null)
            {
                SetDisplayMessagesAndDuration(index);
                _startNewMessageProgram();

                // StartProgram();
            }
            else
            {
                _timer.Stop();
            }
        }

        public PlayPageViewModel(INavigationService navigationService)
        {
            base._navigationService=navigationService;
            _timer = new DispatcherTimer();
            TotalExercisesTime = new TimeSpan(0, 0, 0, 0);

            StartPauseBtnText = STARTBUTTONCONTENT;

            GetMessageCollection();
            
            SetDisplayMessagesAndDuration(0);
        }
        

        private void SetDisplayMessagesAndDuration(int index)
        {
            if (PlayCollection.MessagesTotalCount != 0)
            {
                
                bool isLastMessageActive = index + 1 == PlayCollection.MessageCollection.Count;
                bool isFirstMessageActive = index == 0;
                bool isOnRepeat = PlayCollection.SetToRepeat;

                CurrentMessage = PlayCollection?.MessageCollection[index].MessageText;
           

                if (!isFirstMessageActive)
                {
                    PreviousMessage = PlayCollection?.MessageCollection[index - 1].MessageText;
                    PreviousMesageTimeLeft = PlayCollection.MessageCollection[index - 1].DisplayTime.ToString();
                }


            if (isLastMessageActive)
            {
                if(isOnRepeat)
                {
                    RepeatProgram();
                }
                
            }

                else
                {
                
                    NextMessage = PlayCollection.MessageCollection[index + 1].MessageText;
                    NextMessageTimeLeft = PlayCollection.MessageCollection[index + 1].DisplayTime.ToString();               
                }
            }

        }


        private void RepeatProgram()
        {
            currentMessageIndex = 0;
            CurrentMessage = PlayCollection.MessageCollection[currentMessageIndex].MessageText;
            CurrentTimeLeft = PlayCollection.MessageCollection[currentMessageIndex].DisplayTime.ToString();
            NextMessage = PlayCollection.MessageCollection[currentMessageIndex + 1].MessageText;
            NextMessageTimeLeft = PlayCollection.MessageCollection[currentMessageIndex + 1].DisplayTime.ToString();
            PreviousMessage = string.Empty;
            PreviousMesageTimeLeft = string.Empty;
        }
        

        private void GetMessageCollection()
        {
            Messenger.Default.Register<MessageSetTable>(
                this, messageSet =>
                    {
                            PlayCollection = new MessageSetTable{Description = messageSet.Description,
                            MessageCollection = new ObservableCollection<MessageTable>(messageSet.MessageCollection),
                            MessagesTotalCount = messageSet.MessagesTotalCount,
                            ProgramTotalTime = messageSet.ProgramTotalTime,
                            SetId = messageSet.SetId,
                            SetToRepeat = messageSet.SetToRepeat};

                        CurrentMessage = PlayCollection?.MessageCollection[0].MessageText;
                        CurrentTimeLeft = messageSet.MessageCollection[0].DisplayTime.ToString();
                    });
        }
    }
}
