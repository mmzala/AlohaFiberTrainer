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
    
    private List<ScoreboardElement> elements;
    private List<ScoreboardElement> compareElements;

    private void Start()
    {
        FillList();
    }

    /// <summary>
    /// Tells the scoreboard what scores to compare
    /// </summary>
    /// <param name="element"> Element you want to compare </param>
    /// <param name="add"> If you want to add or remove comparison element </param>
    public void CompareScores(ScoreboardElement element, bool add)
    {
        if(add)
        {
            compareElements.Add(element);
        }
        else
        {
            compareElements.Remove(element);
        }

        Debug.Log(compareElements.Count);
        if(compareElements.Count >= 2)
        {
            SetComparisonElementsActive(false);
        }
        else
        {
            SetComparisonElementsActive(true);
        }
    }

    private void FillList()
    {
        List<PlayerScore> scores = ScoreFileManager.LoadFiles();
        elements = new List<ScoreboardElement>();
        compareElements = new List<ScoreboardElement>();

        foreach (PlayerScore playerScore in scores)
        {
            GameObject scoreElement = Instantiate(scoreElementTemplate);

            ScoreboardElement scoreboardElement = scoreElement.GetComponent<ScoreboardElement>();
            scoreboardElement.SetupElement(this, playerScore);
            elements.Add(scoreboardElement);

            // worldPositionStays is false to fit the object into the list
            scoreElement.transform.SetParent(listContent.transform, false);
        }
    }

    private void SetComparisonElementsActive(bool value)
    {
        foreach(ScoreboardElement element in elements)
        {
            if (compareElements.Contains(element)) continue;
            element.gameObject.SetActive(value);
        }
    }
}
