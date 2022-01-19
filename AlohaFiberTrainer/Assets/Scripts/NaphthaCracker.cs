using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    #region NaphthaCrackerControllers
    public enum Controllers
    {
        Pump,
        Preheater,
        Cooler,
    }

    [Header("Controllers")]
    public Slider pump;
    public Slider preheater;
    public Button cooler;
    #endregion // NaphthaCrackerControllers

    [Header("Malfunction Settings")]
    [SerializeField]
    private List<Malfunction> malfunctions;

    [SerializeField, Min(5f)]
    [Tooltip("How long shoud it take to get to the next malfunction")]
    private Vector2 malfunctionWaitingTime = new Vector2(5f, 5f);

    private float malfunctionTimer;
    private Malfunction currentMalfunction = null;
    private int currentSolutionNeeded;
    private Controllers usedController;

    private void Start()
    {
        SetMalfunctionTimer();
    }

    private void Update()
    {
        if(malfunctionTimer > 0f)
        {
            malfunctionTimer -= Time.deltaTime;
            return;
        }

        ActivateMalfunction();
    }

    #region MalfunctionLogic
    private void ActivateMalfunction()
    {
        if(currentMalfunction == null)
        {
            int randomIndex = Random.Range(0, malfunctions.Count);
            currentMalfunction = malfunctions[randomIndex];
            currentSolutionNeeded = 0;
            ActivateIndicators();
        }
    }

    private void UpdateMalfunction()
    {
        currentSolutionNeeded++;
        if (currentSolutionNeeded >= currentMalfunction.solutions.Count)
        {
            Debug.Log("Malfunction completed!");
            ResetMalfunction();
        }
    }

    private void ActivateIndicators()
    {
        if (!currentMalfunction) return;

        foreach(Malfunction.Indicator indicator in currentMalfunction.indicators)
        {
            int partIndex = (int)indicator.part;
            parts[partIndex].ChangeState(indicator.type, indicator.state);
        }
    }

    /// <summary>
    /// Resets all vailables that control the malfunctions
    /// </summary>
    private void ResetMalfunction()
    {
        currentMalfunction = null;
        currentSolutionNeeded = 0;

        foreach (NaphthaCrackerPart part in parts)
        {
            part.ResetAllElements();
        }

        pump.value = (int)LevelState.Middle;
        preheater.value = (int)LevelState.Middle;

        SetMalfunctionTimer();
    }
    #endregion // MalfunctionLogic

    #region SolutionChecking
    public void SliderSolution(float value)
    {
        if (!currentMalfunction) return;

        int iValue = (int)value;
        Malfunction.Solution currentSolution = currentMalfunction.solutions[currentSolutionNeeded];
        if(currentSolution.controller == usedController
            && (int)currentSolution.state == iValue)
        {
            UpdateMalfunction();
        }
        else
        {
        }
    }

    public void ButtonSolution()
    {
        if (!currentMalfunction) return;

        Malfunction.Solution currentSolution = currentMalfunction.solutions[currentSolutionNeeded];
        if (currentSolution.controller == usedController
            && currentSolution.state == LevelState.Middle)
        {
            UpdateMalfunction();
        }
        else
        {
        }
    }
    #endregion // SolutionChecking

    #region Setters
    public void SetUsedController(int controllerIndex)
    {
        usedController = (Controllers)controllerIndex;
    }

    private void SetMalfunctionTimer()
    {
        malfunctionTimer = Random.Range(malfunctionWaitingTime.x, malfunctionWaitingTime.y);
    }
    #endregion // Setters
}
