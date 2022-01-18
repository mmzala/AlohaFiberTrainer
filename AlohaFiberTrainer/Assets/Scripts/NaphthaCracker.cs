using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaphthaCracker : MonoBehaviour
{
    #region NaphthaCrackerParts
    public enum PartType
    {
        preheaterLeft,
        preheaterRight,
        distillationColumn1,
        distillationColumn2,
        distillationColumn3,
        pump,
        oil,
    }

    [SerializeField]
    private NaphthaCrackerPart[] parts;
    #endregion // NaphthaCrackerParts

    private void Start()
    {
        foreach(var part in parts)
        {
            part.ChangeState(ElementType.Q, LevelState.High);
        }
    }
}
