using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public static class AudioManager
    {
        private static string currentSong = "";

        public static void PlaySoundEffect(string soundEffect, float volume)
        {
            SoundEffect sound = ResourceManager.GetSoundEffect(soundEffect);

            sound.Play(volume, 0, 0);
        }

        public static void PlaySong(string songName)
        {
            if (songName != currentSong || MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(ResourceManager.GetSong(songName));
                MediaPlayer.IsRepeating = true;
            }

            currentSong = songName;
        }

        public static void SetVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }

        public static void Stop()
        {
            MediaPlayer.Stop();
            currentSong = "";
        }

        public static void Pause()
        {
            MediaPlayer.Pause();
        }

        public static void Resume()
        {
            MediaPlayer.Resume();
        }
    }
}
