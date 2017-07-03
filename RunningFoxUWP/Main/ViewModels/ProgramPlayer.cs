using Main.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Main.ViewModels
{
    public class ProgramPlayer : IProgramPlayer
    {
        private DispatcherTimer _timer;
        public List<MessageTable> PlayCollection { get; set; }

        public ProgramPlayer(List<MessageTable> collection)
        {
            PlayCollection = collection;
        }

        public void PlayProgram()
        {
            _timer.Start();
        }

        public void StopProgram()
        {
            _timer.Stop();
        }

        public string DisplayTime()
        {
            return null;
        }
    }
}
