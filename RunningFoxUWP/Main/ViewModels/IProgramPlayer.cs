using Main.Models;
using System.Collections.Generic;

namespace Main.ViewModels
{
    public interface IProgramPlayer
    {
        List<MessageTable> PlayCollection { get; set; }
        void PlayProgram();
        void StopProgram();
        string DisplayTime();
    }
}
