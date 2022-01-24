using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardElement : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Text malfunctionSolvedText;

    [SerializeField]
    private Text reasonScoreText;

    [SerializeField]
    private Text recognitionScoreText;

    public void SetupElement(PlayerScore playerScore)
    {
        playerName.text = playerScore.playerName;
        malfunctionSolvedText.text = playerScore.malfunctionsSolved.ToString();
        reasonScoreText.text = playerScore.malfunctionReasonScore.ToString();
        recognitionScoreText.text = playerScore.malfunctionRecognitionTime.ToString();
    }
}
