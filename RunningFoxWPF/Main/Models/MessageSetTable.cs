using Main.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class MessageSetTable: NotifyService
    {
        private Guid _setID;
        private string _description;




        //private string _name;
        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}



        public Guid SetID
        {
            get { return _setID; }
            set { _setID = value; OnPropertyChanged(); }
        }

     
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

    }
}
