using Main.Shared;
using System;
using System.Collections.ObjectModel;

namespace Main.Models
{
    public class MessageSetTable : NotifyService
    {
        private Guid _setID;
        private string _description;

            
        public Guid SetID
        {
            get { return _setID; }
            set { _setID = value; }
        }


        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
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


        private bool _setToRepeat;
        public bool SetToRepeat
        {
            get { return _setToRepeat; }
            set { _setToRepeat = value; OnPropertyChanged();}
        }
        
        public ObservableCollection<MessageTable> MessageCollection { get; set;}
    }
}
