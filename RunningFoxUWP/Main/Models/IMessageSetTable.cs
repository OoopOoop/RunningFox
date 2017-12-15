using System;
using System.Collections.ObjectModel;

namespace Main.Models
{
    public interface IMessageSetTable
    {
        Guid SetId { get; set; }
        string Description { get; set; }
        int MessagesTotalCount { get; set; }
        int ProgramTotalTime { get; set; }
        bool SetToRepeat { get; set; }
        ObservableCollection<MessageTable> MessageCollection { get; set; }
    }
}