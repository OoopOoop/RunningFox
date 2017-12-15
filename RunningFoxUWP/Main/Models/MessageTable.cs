using System;
using Main.Shared;

namespace Main.Models
{
    public class MessageTable : NotifyService
    {
        public Guid GuidId { get; set; }
     
        public int SortOrder { get; set; }

        private string _displayTimeText;

        public string DisplayTimeText
        {
            get { return _displayTimeText; }
            set { _displayTimeText = value; OnPropertyChanged(); }
        }

        private TimeSpan _displayTime;
        public TimeSpan DisplayTime
        {
            get => _displayTime;
            set { _displayTime = value;
                DisplayTimeText = _formatTime();
                OnPropertyChanged();
            }
        }
        
        private string _messageText;
        public string MessageText
        {
            get => _messageText;
            set {
                _messageText = value;
                OnPropertyChanged();
            }
        }
        
        private string _formatTime() => DisplayTime.Hours == 0 ? $"{DisplayTime.Minutes} minutes" : $"{DisplayTime.Hours} {"hour"} {DisplayTime.Minutes} {"minutes"}";
    }
}
