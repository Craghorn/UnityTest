using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
 //singl
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler instance {  get; set; }

    string dataFileName = "Assets/Config/Config.txt";

    string line;
    bool ischecked = false; // check if the Address line is read

    private void Awake()
    {
        instance = this;
    }

    public void SaveData()
    {
        try
        {
            using (FileStream stream = new FileStream(dataFileName, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write($"Адрес сервера: {SettingsHandler.serverAddress}\n" + $"Порт: {SettingsHandler.serverPort}");
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void LoadData()
    {

        if (File.Exists(dataFileName))
        {
            try
            {
                using (FileStream stream = new FileStream(dataFileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            // if true, this is Port line
                            if (ischecked)
                            {
                                // get Port
                                // call Trim to remove extra spaces
                                SettingsHandler.serverPort = line.Split(": ")[1].Trim();
                                // reset boolean
                                ischecked = false;
                            }

                            // read Port line, next line is the corresponding Address
                            if (line.Split(": ")[0] == "Адрес сервера")
                            {
                                SettingsHandler.serverAddress = line.Split(": ")[1].Trim();
                                // set boolean to true
                                ischecked = true;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
        }
    }
}
