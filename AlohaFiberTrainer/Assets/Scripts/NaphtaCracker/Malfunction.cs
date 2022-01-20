using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMalfunction", menuName = "Malfunction", order = 1)]
public class Malfunction : ScriptableObject
{
    [TextArea(10, 15)]
    public string description;

    #region SolutionSettings
    [System.Serializable]
    public class Solution
    {
        public NaphthaCracker.Controllers controller = 0;

        [Tooltip("If the controller is a button, this doesn't matter anymore")]
        public LevelState state = LevelState.Middle;

        [Min(0f)]
        [Tooltip("How long should the player wait before this solution")]
        public float delay = 0f;
    }

    [Header("Solutions")]
    public List<Solution> solutions;

    [Min(5f)]
    [Tooltip("Maximum time to solve the malfunction in seconds")]
    public float timeToSolve = 10f;
    #endregion // SolutionSettings

    #region IndicatorSettings
    [System.Serializable]
    public class Indicator
    {
        public NaphthaCracker.PartType part;
        public ElementType type;
        public LevelState state;
    }

    [Header("Indicators")]
    public List<Indicator> indicators;
    #endregion // IndicatorSettings
}
