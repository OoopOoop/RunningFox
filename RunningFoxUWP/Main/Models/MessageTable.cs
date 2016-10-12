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
        //private SolidColorBrush _colorForeground;
        //private SolidColorBrush _colorBackground;




        public Guid SetID { get; set; }
      
        public Guid MessageID { get; set; }
    
        public int SortOrder { get; set; }
      
        public TimeSpan DisplayTime { get; set; }
  
        public string DisplayTimeText => _formatText();


        public string MessageText { get; set; }
   
     //   public SolidColorBrush ColorForeground { get; set; }
        //{
        //    get { return _colorForeground; }
        //    set { _colorForeground = value; OnPropertyChanged(); }
        //}

     //   public SolidColorBrush ColorBackground { get; set; }
        //{
        //    get { return _colorForeground; }
        //    set { _colorForeground = value; OnPropertyChanged(); }
        //}



        private string _formatText()
        {
            //string time="";

            //if(DisplayTime.Hours==0)
            //{
            //    time = $"{DisplayTime.Minutes} {"minutes"}";
            //}
            //return time;


            return DisplayTime.Hours == 0 ? $"{DisplayTime.Minutes} {"minutes"}" : $"{DisplayTime.Hours} {"hour"} {DisplayTime.Minutes} {"minutes"}";


        }
    }
}
