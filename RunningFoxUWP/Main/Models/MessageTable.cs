using Main.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Main.Models
{
    public class MessageTable : NotifyService
    {
        //private Guid _setID;
        //private Guid _messageID;
        //private int _sortOrder;
        //private TimeSpan _displayTime;
        //private string _messageText;
        //private SolidColorBrush _colorForeground;
        //private SolidColorBrush _colorBackground;


        public Guid SetID { get; set; }
        //{
        //    get { return _setID; }
        //    set { _setID = value; OnPropertyChanged(); }
        //}

        public Guid MessageID { get; set; }
        //{
        //    get { return _messageID; }
        //    set { _messageID = value; OnPropertyChanged(); }
        //}

        public int SortOrder { get; set; }
        //{
        //    get { return _sortOrder; }
        //    set { _sortOrder = value; OnPropertyChanged(); }
        //}

        public TimeSpan DisplayTime { get; set; }
        //{
        //    get { return _displayTime; }
        //    set { _displayTime = value; OnPropertyChanged(); }
        //}

        public string MessageText { get; set; }
        //{
        //    get { return _messageText; }
        //    set { _messageText = value; OnPropertyChanged(); }
        //}

        public SolidColorBrush ColorForeground { get; set; }
        //{
        //    get { return _colorForeground; }
        //    set { _colorForeground = value; OnPropertyChanged(); }
        //}

        public SolidColorBrush ColorBackground { get; set; }
        //{
        //    get { return _colorForeground; }
        //    set { _colorForeground = value; OnPropertyChanged(); }
        //}
    }
}
