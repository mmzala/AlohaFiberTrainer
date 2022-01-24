using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private List<PlayerScore> scores;

    private void Start()
    {
        PlayerScore score = ScriptableObject.CreateInstance<PlayerScore>();
        score.playerName = "marcin";
        score.malfunctionRecognitionTime = 19.5f;
        score.malfunctionReasonScore = 5;
        score.malfunctionsSolved = 6;

        ScoreFileManager.SaveFile(score);

        scores = ScoreFileManager.LoadFiles();
        foreach (PlayerScore pScore in scores)
        {
            Debug.Log(pScore.playerName);
            Debug.Log("-----------------------------------------------");
        }
    }
}
