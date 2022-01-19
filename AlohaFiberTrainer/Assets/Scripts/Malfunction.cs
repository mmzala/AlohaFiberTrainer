using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMalfunction", menuName = "Malfunction", order = 1)]
public class Malfunction : ScriptableObject
{
    #region SolutionSettings
    [System.Serializable]
    public class Solution
    {
        public NaphthaCracker.Controllers controller = 0;

        [Tooltip("If the controller is a button, this doesn't matter anymore")]
        public LevelState state = LevelState.Middle;

        [Min(0f)]
        [Tooltip("How long should the player wait before the next solution")]
        public float delay = 0f;
    }

    [Header("Solutions")]
    public List<Solution> solutions;

    [Min(5f)]
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
