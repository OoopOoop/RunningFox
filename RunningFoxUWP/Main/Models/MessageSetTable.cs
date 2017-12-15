using System;
using System.Collections.ObjectModel;

namespace Main.Models
{
    public class MessageSetTable : IMessageSetTable
    {
        public Guid SetId { get; set; }

        public string Description { get; set; }

        public int MessagesTotalCount { get; set; }

        public int ProgramTotalTime { get; set; }

        public bool SetToRepeat { get; set; }

        public ObservableCollection<MessageTable> MessageCollection { get; set;}
    }
}
