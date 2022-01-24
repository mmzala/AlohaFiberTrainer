using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
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
        for(int i = 0; i < 10; i++)
        {
            GameObject scoreElement = Instantiate(scoreElementTemplate);

            scoreElement.transform.SetParent(listContent.transform, false);
        }
    }
}
