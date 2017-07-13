using Main.Models;
using System;
using System.Collections.Generic;

namespace Main.ViewModels
{
    public interface IProgramPlayer
    {
        TimeSpan CurrentMessageTime { get; set; }
        TimeSpan PreviousMessageTime { get; set; }
        TimeSpan NextMessageTime { get; set; }
        TimeSpan TotalExerciseTime { get; set; }
        string CurrentMessage { get; set; }
        string PreviousMessage { get; set; }
        string NextMessage { get; set; }
        string PlayButtonContext { get; set; }
        void SetProgram(MessageSetTable PlayCollection);
        void PlayProgram(MessageTable messageToPlay);
        void StopProgram();
    }
}
