using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CrowEngineBase
{
    public static class InputPersistence
    {
        private static bool isLoading = false;
        private static bool isSaving = false;
        public static KeyboardInput keyboardInput = new KeyboardInput();
        public static MouseInput mouseInput = new MouseInput();
        public static ControllerInput controllerInput = new ControllerInput();


        public static void SaveKeyboardControls(KeyboardInput input)
        {
            if (!isSaving)
            {
                isSaving = true;
                FinalizeAsyncKeyboardSave(input);
            }
        }

        private static async void FinalizeAsyncKeyboardSave(InputComponent input)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("KeyboardControls.xml", FileMode.Create))
                        {
                            if (fs != null)
                            {

                                using (var isoFileWriter = new StreamWriter(fs))
                                {
                                    isoFileWriter.WriteAsync(JsonConvert.SerializeObject(input));
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


        public static void SaveMouseControls(MouseInput input)
        {
            if (!isSaving)
            {
                isSaving = true;
                FinalizeAsyncMouseSave(input);
            }
        }

        private static async void FinalizeAsyncMouseSave(MouseInput input)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("MouseControls.xml", FileMode.Create))
                        {
                            if (fs != null)
                            {

                                using (var isoFileWriter = new StreamWriter(fs))
                                {
                                    isoFileWriter.WriteAsync(JsonConvert.SerializeObject(input));
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

        public static void SaveControllerControls(ControllerInput input)
        {
            if (!isSaving)
            {
                isSaving = true;
                FinalizeAsyncControllerSave(input);
            }
        }

        private static async void FinalizeAsyncControllerSave(ControllerInput input)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("ControllerControls.xml", FileMode.Create))
                        {
                            if (fs != null)
                            {

                                using (var isoFileWriter = new StreamWriter(fs))
                                {
                                    isoFileWriter.WriteAsync(JsonConvert.SerializeObject(input));
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



        public static void LoadControls()
        {
            LoadControllerControls();
            LoadMouseControls();
            LoadKeyboardControls();
        }


        public static void LoadKeyboardControls()
        {
            if (!isLoading)
            {
                isLoading = true;
                FinalizeAsyncKeyboardLoad();
            }

        }


        private static async void FinalizeAsyncKeyboardLoad()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("KeyboardControls.xml", FileMode.Open))
                        {
                            if (fs != null)
                            {

                                using (var isoFileReader = new StreamReader(fs))
                                {
                                    keyboardInput = JsonConvert.DeserializeObject<KeyboardInput>(isoFileReader.ReadToEnd());
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
            });
        }



        public static void LoadMouseControls()
        {
            if (!isLoading)
            {
                isLoading = true;
                FinalizeAsyncMouseLoad();
            }
        }

        private static async void FinalizeAsyncMouseLoad()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("MouseControls.xml", FileMode.Open))
                        {
                            if (fs != null)
                            {

                                using (var isoFileReader = new StreamReader(fs))
                                {
                                    mouseInput = JsonConvert.DeserializeObject<MouseInput>(isoFileReader.ReadToEnd());
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
            });
        }



        public static void LoadControllerControls()
        {
            if (!isLoading)
            {
                isLoading = true;
                FinalizeAsyncControllerLoad();
            }
        }

        private static async void FinalizeAsyncControllerLoad()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("ControllerControls.xml", FileMode.Open))
                        {
                            if (fs != null)
                            {

                                using (var isoFileReader = new StreamReader(fs))
                                {
                                    controllerInput = JsonConvert.DeserializeObject<ControllerInput>(isoFileReader.ReadToEnd());
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
            });
        }




    }
}
