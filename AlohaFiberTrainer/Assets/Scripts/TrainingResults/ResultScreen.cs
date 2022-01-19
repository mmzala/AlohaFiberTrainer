using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    #region ResultScreenComponents
    [Header("Result Screen Components")]
    [SerializeField]
    private Text malfunctionsSolvedText;

    [SerializeField]
    private Text timePlayedText;

    [SerializeField]
    private Text malfunctionNameText;

    [SerializeField]
    private Text malfunctionDescriptionText;
    #endregion // ResultScreenComponents

    [Header("Timer")]
    [SerializeField]
    private Clock clock;

    [SerializeField]
    [Tooltip("Objects that are set inavtive when result screen is shown")]
    private List<GameObject> objectsToHide;

    /// <summary>
    /// Shows results of the training
    /// </summary>
    /// <param name="malfunction"> The malfunction that you failed at </param>
    /// <param name="score"> The number of solved malfunctions </param>
    public void ShowResult(Malfunction malfunction, int score)
    {
        SetObjectsToHide(false);
        SetUpResultScreen(malfunction, score);
        gameObject.SetActive(true);
    }

    private void SetUpResultScreen(Malfunction malfunction, int score)
    {
        malfunctionsSolvedText.text = score.ToString();
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
