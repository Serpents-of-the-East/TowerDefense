using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

using Microsoft.Xna.Framework.Input;

namespace CrowEngineBase
{
    public static class InputPersistence
    {

        private static bool isLoadingKeyboard = false;
        private static bool isLoadingMouse = false;
        private static bool isLoadingController = false;

        private static bool isSavingKeyboard = false;
        private static bool isSavingMouse = false;
        private static bool isSavingController = false;

        public static KeyboardInput keyboardInput = new KeyboardInput();
        public static MouseInput mouseInput = new MouseInput();
        public static ControllerInput controllerInput = new ControllerInput();

        public static bool keyboardLoaded { get; private set; }
        public static bool mouseLoaded { get; private set; }
        public static bool controllerLoaded { get; private set; }

        public static bool controlsLoaded { get
            {
                return keyboardLoaded && mouseLoaded && controllerLoaded;
            } }

        static InputPersistence()
        {
            keyboardLoaded = false;
            mouseLoaded = false;
            controllerLoaded = false;
        }

        public static void LoadSavedKeyboard(ref KeyboardInput keyboard)
        {
            if (!controlsLoaded)
            {
                Debug.WriteLine("Warning: Saved keyboard state has not finished loading yet...");
                return;
            }

            foreach((string action, Keys key) in keyboardInput.actionKeyPairs)
            {
                if (keyboard.actionKeyPairs.ContainsKey(action))
                {
                    keyboard.actionKeyPairs[action] = key;
                }
                else
                {
                    Debug.WriteLine($"Action {action} could not be found in the given keyboard input. You should ensure this is not a mistake");
                }
            }
        }


        public static void SaveKeyboardControls(KeyboardInput input)
        {
            if (!isSavingKeyboard)
            {
                isSavingKeyboard = true;
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

                isSavingKeyboard = false;
            });
        }


        public static void SaveMouseControls(MouseInput input)
        {
            if (!isSavingMouse)
            {
                isSavingMouse = true;
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

                isSavingMouse = false;
            });
        }

        public static void SaveControllerControls(ControllerInput input)
        {
            if (!isSavingController)
            {
                isSavingController = true;
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

                isSavingController = false;
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
            if (!isLoadingKeyboard)
            {
                keyboardLoaded = false;
                isLoadingKeyboard = true;
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

                isLoadingKeyboard = false;
                keyboardLoaded = true;
            });
        }



        public static void LoadMouseControls()
        {
            if (!isLoadingMouse)
            {
                mouseLoaded = false;
                isLoadingMouse = true;
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

                isLoadingMouse = false;
                mouseLoaded = true;
            });
        }



        public static void LoadControllerControls()
        {
            if (!isLoadingController)
            {
                controllerLoaded = false;
                isLoadingController = true;
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

                isLoadingController = false;
                controllerLoaded = true;
            });
        }




    }
}
