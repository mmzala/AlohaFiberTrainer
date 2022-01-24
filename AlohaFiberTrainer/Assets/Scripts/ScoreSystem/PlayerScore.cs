using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : ScriptableObject
{
    public string playerName = "Player";

    // Average time in which the player recognized malfunctions
    public float malfunctionRecognitionTime = 0f;
    public int malfunctionReasonScore = 0;
    public int malfunctionsSolved = 0;
}
