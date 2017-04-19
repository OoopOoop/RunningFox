using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Main.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Main.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private ObservableCollection<MessageSetTable> _messageSetCollection;
        public ObservableCollection<MessageSetTable> MessageSetCollection
        {
            get { return _messageSetCollection; }
            set { _messageSetCollection = value; OnPropertyChanged(); }
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


        private string _programDifficulty;
        public string ProgramDifficulty
        {
            get { return _programDifficulty; }
            set { _programDifficulty = value;OnPropertyChanged(); }
        }
        

        public MainViewModel(INavigationService navigationService)
        {
            base._navigationService = navigationService;
            MessageSetCollection = new ObservableCollection<MessageSetTable>();
            getMessageSets();
        }

        private RelayCommand<MessageSetTable> _editProgramCommand;
        public RelayCommand<MessageSetTable> EditProgramCommand => _editProgramCommand ?? (_editProgramCommand = new RelayCommand<MessageSetTable>(EditProgram));

        private void EditProgram(MessageSetTable messageSet)
        {

            var toRemoveCollection = new ObservableCollection<MessageSetTable>();
            toRemoveCollection.Add(messageSet);

            //_navigationService.NavigateTo("EditSet",messageSet);
            _navigationService.NavigateTo("EditSet");
            Messenger.Default.Send(toRemoveCollection);
        }


        private RelayCommand _removeProgramCommand;
        public RelayCommand RemoveProgramCommand => _removeProgramCommand ?? (_removeProgramCommand = new RelayCommand(RemoveProgram));
        
        private void RemoveProgram()
        {
        }

        private RelayCommand _playProgramCommand;
        public RelayCommand PlayProgramCommand => _playProgramCommand ?? (_playProgramCommand = new RelayCommand(PlayProgram));

        private void PlayProgram()
        {
        }

        
        private void getMessageSets()
        {
           Messenger.Default.Register<MessageSetTable>(
           this,
           messageSet =>
           {
               var collection = MessageSetCollection.Where(x => x.SetID == messageSet.SetID).FirstOrDefault();
               if(collection!=null)
               {
                   int indexOfCollection = MessageSetCollection.IndexOf(messageSet);
                   MessageSetCollection[indexOfCollection] = messageSet;
               }
               else
               {
                   MessageSetCollection.Add(messageSet);
               }
            
           });
        }


        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    Messenger.Default.Unregister<StatusMessage>(
        //      this, HandleStatusMessage);
        //    base.OnNavigatingFrom(e);
        //}
    }
}
