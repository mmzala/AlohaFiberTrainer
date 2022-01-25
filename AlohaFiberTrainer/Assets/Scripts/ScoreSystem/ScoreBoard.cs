using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [Header("Scrollable List Settings")]
    [SerializeField]
    private GameObject listContent;

    [SerializeField]
    private GameObject scoreElementTemplate;
    
    private List<PlayerScore> scores;

    private void Start()
    {
        scores = ScoreFileManager.LoadFiles();
        FillList();
    }

    private void FillList()
    {
        foreach (PlayerScore playerScore in scores)
        {
            GameObject scoreElement = Instantiate(scoreElementTemplate);

            ScoreboardElement scoreboardElement = scoreElement.GetComponent<ScoreboardElement>();
            scoreboardElement.SetupElement(playerScore);

            // worldPositionStays is false to fit the object into the list
            scoreElement.transform.SetParent(listContent.transform, false);
        }
    }
}
