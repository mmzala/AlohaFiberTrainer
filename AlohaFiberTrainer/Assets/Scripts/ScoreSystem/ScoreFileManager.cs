using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class ScoreFileManager
{
    private const string fileExtension = ".score";

    public static List<PlayerScore> LoadFiles()
    {
        List<PlayerScore> scores = new List<PlayerScore>();

        // Get all score files
        string path = "C:/GitHub/AlohaFiberTrainer/AlohaFiberTrainer/Assets/Scores";
        string[] files = Directory.GetFiles(path, "*" + fileExtension);

        foreach (string file in files)
        {
            FileStream fileStream = File.Open(file, FileMode.Open);
            fileStream.Dispose(); // Dispose file, otherwise there will be a path violation error
            PlayerScore score = ScriptableObject.CreateInstance<PlayerScore>();
            
            string fileContents = File.ReadAllText(file);
            JsonUtility.FromJsonOverwrite(fileContents, score);

            scores.Add(score);
            fileStream.Close();
        }

        return scores;
    }

    public static void SaveFile(PlayerScore playerScore)
    {
        // Create file
        string path = "C:/GitHub/AlohaFiberTrainer/AlohaFiberTrainer/Assets/Scores/" + playerScore.playerName + DateTime.Now.ToString("_MMddyyyy_HHmmss") + fileExtension;
        FileStream file = File.Create(path);
        file.Dispose(); // Dispose file, otherwise there will be a path violation error

        // Save score in file
        string json = JsonUtility.ToJson(playerScore);
        File.WriteAllText(path, json);
        file.Close();
    }
}
