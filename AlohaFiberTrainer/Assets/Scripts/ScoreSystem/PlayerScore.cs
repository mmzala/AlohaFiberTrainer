using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : ScriptableObject
{
    public string playerName;

    // Average time in which the player recognized malfunctions
    public float malfunctionRecognitionTime;
    public int malfunctionReasonScore;
    public int malfunctionsSolved;
}
