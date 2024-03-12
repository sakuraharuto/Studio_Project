using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SaveLoadSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string SaveDirectory = "/SaveData/";
        public const string FileName = "SaveGame.sav";

        public static bool SaveGameData()
        {   
            var dir = Application.persistentDataPath + SaveDirectory;

            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = JsonUtility.ToJson(CurrentSaveData, true);

            File.WriteAllText(dir + FileName, json);

            GUIUtility.systemCopyBuffer = dir;  //copy directory

            return true;
        }

        public static void LoadGameData()
        {
            string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
            
            SaveData tempData = new SaveData();

            if(File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.LogError("Save file does not exist!");
            }
            
            CurrentSaveData = tempData; 

        }
    }
}