using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMalfunction", menuName = "Malfunction", order = 1)]
public class Malfunction : ScriptableObject
{
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

    [System.Serializable]
    public class Indicator
    {
        public NaphthaCracker.PartType part;
        public ElementType type;
        public LevelState state;
    }

    [Header("Indicators")]
    public List<Indicator> indicators;
}
