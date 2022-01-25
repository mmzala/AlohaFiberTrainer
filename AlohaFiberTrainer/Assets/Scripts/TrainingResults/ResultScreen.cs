using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    #region ResultScreenComponents
    [Header("Text Components")]
    [SerializeField]
    private Text malfunctionsSolvedText;

    [SerializeField]
    private Text timePlayedText;

    [SerializeField]
    private Text malfunctionNameText;

    [SerializeField]
    private Text malfunctionDescriptionText;
    #endregion // ResultScreenComponents

    [Header("Name Input")]
    [SerializeField]
    private InputField input;

    [Header("Timer")]
    [SerializeField]
    private Clock clock;

    [SerializeField]
    [Tooltip("Objects that are set inactive when result screen is shown")]
    private List<GameObject> objectsToHide;

    // Player's score
    private PlayerScore score;

    /// <summary>
    /// Shows results of the training
    /// </summary>
    /// <param name="malfunction"> The malfunction that you failed at </param>
    /// <param name="score"> Player's score </param>
    public void ShowResult(Malfunction malfunction, PlayerScore score)
    {
        this.score = score;

        SetObjectsToHide(false);
        SetUpResultScreen(malfunction);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// This should ONLY be called when exiting the result screen
    /// </summary>
    public void SaveScore()
    {
        // If player has given his name, use it, otherwise use the default name
        if(input.text != "")
        {
            score.playerName = input.text;
        }

        // If there are no malfunctions solved then just set recognition time to 0
        if(score.malfunctionsSolved == 0)
        {
            score.malfunctionRecognitionTime = 0f;
        }
        else
        {
            score.malfunctionRecognitionTime /= score.malfunctionsSolved;
        }
        
        ScoreFileManager.SaveFile(score);
    }

    private void SetUpResultScreen(Malfunction malfunction)
    {
        malfunctionsSolvedText.text = score.malfunctionsSolved.ToString();
        timePlayedText.text = clock.GetPlayedTime();
        malfunctionNameText.text = malfunction.name;
        malfunctionDescriptionText.text = malfunction.description;
    }

    private void SetObjectsToHide(bool active)
    {
        foreach(GameObject obj in objectsToHide)
        {
            obj.SetActive(active);
        }
    }
}
