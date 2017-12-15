using System.Collections.ObjectModel;

namespace Main.Models
{
    public class MessageTableCollection: IMessageTableCollection
    {
        public MessageTableCollection()
        {
            Messages= new ObservableCollection<MessageTable>();
        }

        public MessageTable SelectedMessage { get; set; }
        public ObservableCollection<MessageTable> Messages { get; set; }
    }
}
