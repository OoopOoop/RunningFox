using System.Collections.ObjectModel;

namespace Main.Models
{
    public interface IMessageTableCollection
    {
        MessageTable SelectedMessage { get; set; }
        ObservableCollection<MessageTable> Messages { get; set; }
    }
}