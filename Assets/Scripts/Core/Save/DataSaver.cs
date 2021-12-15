﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataSaver
{

    public static string dataFileName = "player.data";

    public static void SaveGameState(int levelId, LevelSaveData levelData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetSaveDataPath();

        PlayerData data = LoadGameState();
        data.Update(levelId, levelData);

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadGameState()
    {

        string path = GetSaveDataPath();
        PlayerData data;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("Player data file not found.");
            data = new PlayerData();
        }
        return data;
    }

    private static string GetSaveDataPath()
    {
        return Application.persistentDataPath + "/" + dataFileName;
    }

    public static bool DeleteSavedData()
    {

        bool result = false;
        string path = GetSaveDataPath();

        if (File.Exists(path))
        {
            File.Delete(path);
            result = true;
        }
        return result;
    }


}
