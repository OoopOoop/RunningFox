using Main.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public ObservableCollection<MessageTable> MessageCollection { get; set;}
    }
}
