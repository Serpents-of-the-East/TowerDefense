using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// Starts and stops songs
    /// </summary>
    public class AudioSystem : System
    {
        public AudioSystem() : base(typeof(AudioSource))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(uint id in m_gameObjects.Keys)
            {
                AudioSource audioSource = m_gameObjects[id].GetComponent<AudioSource>();

                while (audioSource.soundEffectsToPlay.Count > 0)
                {
                    string effectName = audioSource.soundEffectsToPlay.Dequeue();
                    SoundEffect effect = ResourceManager.GetSoundEffect(effectName);

                    effect.Play();
                }

                if (audioSource.previousState == SongState.Stopped && audioSource.currentState == SongState.Playing)
                {
                    MediaPlayer.Play(ResourceManager.GetSong(audioSource.currentSong));
                    MediaPlayer.IsRepeating = audioSource.repeatSong;
                }
                else if (audioSource.previousState == SongState.Paused && audioSource.currentState == SongState.Playing)
                {
                    MediaPlayer.Resume();
                }
                else if (audioSource.previousState == SongState.Playing && audioSource.currentState == SongState.Paused)
                {
                    MediaPlayer.Pause();
                }
                else if (audioSource.previousState == SongState.Playing && audioSource.currentState == SongState.Stopped)
                {
                    MediaPlayer.Stop();
                }

                audioSource.previousState = audioSource.currentState;
                audioSource.previousSong = audioSource.currentSong;

            }
        }
    }
}
