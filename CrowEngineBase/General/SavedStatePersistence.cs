using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CrowEngineBase
{

    public static class SavedStatePersistence
    {

        private static bool isLoading = false;
        private static bool isSaving = false;

        private static Dictionary<string, int> highScores = new Dictionary<string, int>();

        public static bool scoresLoaded { get; private set; }

        /// <summary>
        /// Loads the scores into the given dictionary
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public static bool LoadScoresIntoDictionary(ref Dictionary<string, int> scores)
        {
            if (!scoresLoaded)
            {
                Debug.WriteLine("Warning: Saved score state has not finished loading yet...");
                return false;
            }

            else
            {
                foreach ((string key, int value) in highScores)
                {
                    if (scores.ContainsKey(key))
                    {
                        scores[key] = value;
                    }
                    else
                    {
                        scores.Add(key, value);
                    }
                }

                return true;
            }
        }

        public static void SaveScores(in Dictionary<string, int> scores)
        {
            if (!isSaving)
            {

                foreach ((string name, int score) in scores)
                {
                    highScores[name] = score;
                }

                highScores = scores;
                FinalizeAsyncScoresSave(scores);
            }
        }

        private static async void FinalizeAsyncScoresSave(Dictionary<string, int> scores)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("scores.json", FileMode.Create))
                        {
                            if (fs != null)
                            {

                                using (var isoFileWriter = new StreamWriter(fs))
                                {
                                    isoFileWriter.WriteAsync(JsonConvert.SerializeObject(scores));
                                }
                            }
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        Console.WriteLine($"Failed to save file because of {e}");
                    }
                }

                isSaving = false;
            });
        }

        public static void LoadScores()
        {
            if (!isLoading)
            {
                isLoading = true;
                scoresLoaded = false;
                FinalizeAsyncScoresLoad();
            }
        }

        private static async void FinalizeAsyncScoresLoad()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("scores.json", FileMode.Open))
                        {
                            if (fs != null)
                            {

                                using (var isoFileReader = new StreamReader(fs))
                                {
                                    highScores = JsonConvert.DeserializeObject<Dictionary<string, int>>(isoFileReader.ReadToEnd());
                                }
                            }
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        Console.WriteLine($"Failed to save file because of {e}");
                    }
                }

                isLoading = false;
                scoresLoaded = true;
            });
        }
    }
}
