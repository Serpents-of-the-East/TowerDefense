using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CrowEngineBase
{

    public static class SavedStatePersistence
    {

        private static bool isLoading = false;
        private static bool isSaving = false;

        private static List<TowerDefenseHighScores> highScores = new List<TowerDefenseHighScores>();

        public static bool scoresLoaded { get; private set; }

        /// <summary>
        /// Loads the scores into the given dictionary
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public static bool LoadScoresIntoDictionary(ref List<TowerDefenseHighScores> scores)
        {
            if (!scoresLoaded)
            {
                Debug.WriteLine("Warning: Saved score state has not finished loading yet...");
                return false;
            }

            else
            {
                foreach (TowerDefenseHighScores highScore in scores)
                {
                    if (!highScores.Contains(highScore))
                    {
                        highScores.Add(highScore);
                    }  
                }

                return true;
            }
        }

        public static void SaveScores(in List<TowerDefenseHighScores> scores)
        {
            if (!isSaving)
            {

                highScores = scores;
                FinalizeAsyncScoresSave(scores);
            }
        }

        public static void SaveNewScore(TowerDefenseHighScores score)
        {
            if (!isSaving)
            {
                highScores.Add(score);
                FinalizeAsyncScoresSave(highScores);
            }
        }

        private static async void FinalizeAsyncScoresSave(List<TowerDefenseHighScores> scores)
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
                                    highScores = JsonConvert.DeserializeObject<List<TowerDefenseHighScores>>(isoFileReader.ReadToEnd());
                                }
                            }
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        Console.WriteLine($"Failed to load file because of {e}");
                    }
                }

                isLoading = false;
                scoresLoaded = true;
            });
        }
    }
}
