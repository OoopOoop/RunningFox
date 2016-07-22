using GalaSoft.MvvmLight.Command;
using Main.Models;
using Main.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
   public class MainViewModel:NotifyService
    {
        private ObservableCollection<MessageSetTable> _programCollection;
        public ObservableCollection<MessageSetTable> ProgramCollection
        {
            get { return _programCollection; }
            set { _programCollection = value; OnPropertyChanged(); }
        }

        private RelayCommand<MessageSetTable> _deleteProgramCommand;


        /// <summary>
        /// Remove program
        /// </summary>
        public RelayCommand<MessageSetTable> DeleteProgramCommand => _deleteProgramCommand ?? (_deleteProgramCommand = new RelayCommand<MessageSetTable>(
            messageTable=>
            {
                ProgramCollection.Remove(messageTable);
            }
            ));
      
     



        private RelayCommand<MessageSetTable> _startProgramCommand;

        /// <summary>
        /// Start run
        /// </summary>
        public RelayCommand<MessageSetTable> StartProgramCommand => _startProgramCommand ?? (_startProgramCommand = new RelayCommand<MessageSetTable>(startRun));


        private void startRun(MessageSetTable messageTable)
        {

        }



        private RelayCommand _createNewProgramCommand;
        /// <summary>
        /// Create new run program
        /// </summary>
        public RelayCommand CreateNewProgramCommand => _createNewProgramCommand ?? (_createNewProgramCommand = new RelayCommand(
            ()=>
        {
            _navigationService.NavigateTo("EditPage");
        }));

  


        private RelayCommand<MessageSetTable> _editProgramCommand;
        /// <summary>
        /// Create new run program
        /// </summary>
        public RelayCommand<MessageSetTable> EditProgramCommand => _editProgramCommand ?? (_editProgramCommand = new RelayCommand<MessageSetTable>(editProgram));

        private void editProgram(MessageSetTable messageTable)
        {

        }




        private IFrameNavigationService _navigationService;

        public MainViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;


            ProgramCollection = new ObservableCollection<MessageSetTable>();
            ProgramCollection.Add(new MessageSetTable { Name = "Run1", Description = "very hard program 1", SetID = new Guid() });
            ProgramCollection.Add(new MessageSetTable { Name = "Run2", Description = "very hard program 2", SetID = new Guid() });
            ProgramCollection.Add(new MessageSetTable { Name = "Run3", Description = "very hard program 3", SetID = new Guid() });
        }
    }
}
