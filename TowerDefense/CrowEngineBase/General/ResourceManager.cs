using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public static class ResourceManager
    {
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> music = new Dictionary<string, Song>();
        public static ContentManager manager { get; set; }


        public static SpriteFont GetFont(string fontName)
        {
            if (!fonts.ContainsKey(fontName))
            {
                throw new Exception($"{fontName} doesn't exist in the current resource manager");
            }

            return fonts[fontName];
        }

        public static Texture2D GetTexture(string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                throw new Exception($"{textureName} doesn't exist in the current resource manager");
            }

            return textures[textureName];
        }

        public static Song GetSong(string songName)
        {
            if (!music.ContainsKey(songName))
            {
                throw new Exception($"{songName} doesn't exist in the current resource manager");
            }

            return music[songName];
        }

        public static SoundEffect GetSoundEffect(string soundEffectName)
        {
            if (!music.ContainsKey(soundEffectName))
            {
                throw new Exception($"{soundEffectName} doesn't exist in the current resource manager");
            }
            return soundEffects[soundEffectName];
        }

        public static void RegisterFont(string pathToFont, string fontName)
        {
            fonts.Add(fontName, manager.Load<SpriteFont>(pathToFont));
        }

        public static void RegisterTexture(string pathToTexture, string textureName)
        {
            textures.Add(textureName, manager.Load<Texture2D>(pathToTexture));
        }

        public static void RegisterSoundEffect(string soundEffect, string pathToSoundEffect)
        {
            soundEffects.Add(soundEffect, manager.Load<SoundEffect>(pathToSoundEffect));
        }

        public static void RegisterSong(string song, string pathToSong)
        {
            music.Add(song, manager.Load<Song>(pathToSong));
        }

    }
}
