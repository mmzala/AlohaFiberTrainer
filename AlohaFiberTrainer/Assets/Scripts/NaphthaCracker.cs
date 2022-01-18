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
    public Slider Preheater;
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

    private void ActivateIndicators()
    {
        if (!currentMalfunction) return;

        foreach(Malfunction.Indicator indicator in currentMalfunction.indicators)
        {
            int partIndex = (int)indicator.part;
            parts[partIndex].ChangeState(indicator.type, indicator.state);
        }
    }

    public void SliderSolution(float value)
    {
        int iValue = (int)value;
        Malfunction.Solution currentSolution = currentMalfunction.solutions[currentSolutionNeeded];
        if(currentSolution.controller == usedController
            && (int)currentSolution.state == iValue)
        {
            Debug.Log("Right!");
        }
        else
        {
            Debug.Log("WRONG!");
        }
    }

    public void ButtonSolution()
    {
        Malfunction.Solution currentSolution = currentMalfunction.solutions[currentSolutionNeeded];
        if (currentSolution.controller == usedController
            && currentSolution.state == LevelState.Middle)
        {
            Debug.Log("Right!");
        }
        else
        {
            Debug.Log("WRONG!");
        }
    }

    public void SetUsedController(int controllerIndex)
    {
        usedController = (Controllers)controllerIndex;
    }

    private void SetMalfunctionTimer()
    {
        malfunctionTimer = Random.Range(malfunctionWaitingTime.x, malfunctionWaitingTime.y);
    }
}
