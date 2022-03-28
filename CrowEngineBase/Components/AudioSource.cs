using System;
using System.Collections.Generic;

namespace CrowEngineBase
{

    public enum SongState
    {
        Paused,
        Playing,
        Stopped
    }

    public class AudioSource : Component
    {

        public SongState previousState { get; set; }
        public SongState currentState { get; set; } 
        public string previousSong { get; set; }
        public string currentSong { get; set; }
        public bool repeatSong { get; set; }
        public Queue<string> soundEffectsToPlay { get; set; }

        public AudioSource()
        {
            currentState = SongState.Stopped;
            previousState = SongState.Stopped;
            currentSong = "";
            previousSong = "";
            repeatSong = false;
            soundEffectsToPlay = new Queue<string>();
        }


    }
}
