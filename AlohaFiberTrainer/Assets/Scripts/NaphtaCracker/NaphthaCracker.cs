using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    #region MalfunctionSettings
    [Header("Malfunction Settings")]
    [SerializeField]
    private List<Malfunction> malfunctions;

    [SerializeField, Min(5f)]
    [Tooltip("How long shoud it take to get to the next malfunction")]
    private Vector2 malfunctionWaitingTime = new Vector2(5f, 5f);

    private float malfunctionTimer;
    private float malfunctionCompleteTimer;
    private float currentSolutionDelay;
    private Malfunction currentMalfunction = null;
    private int currentSolutionNeeded = 0;
    private Controllers usedController;
    #endregion // MalfunctionSettings

    [Header("Score Settings")]
    [SerializeField]
    private ResultScreen resultScreen;

    private PlayerScore playerScore;

    [Header("Sound Effects")]
    [SerializeField]
    private AudioSource correctSound;

    [SerializeField]
    private AudioSource wrongSound;

    private void Start()
    {
        playerScore = ScriptableObject.CreateInstance<PlayerScore>();
        SetMalfunctionTimer();
    }

    private void Update()
    {
        // Wait some time between different malfunctions
        if(malfunctionTimer > 0f)
        {
            malfunctionTimer -= Time.deltaTime;
            return;
        }

        ActivateMalfunction();
        UpdateMalfunctionTimers();
    }

    #region MalfunctionLogic
    private void ActivateMalfunction()
    {
        if(currentMalfunction == null)
        {
            int randomIndex = UnityEngine.Random.Range(0, malfunctions.Count);
            currentMalfunction = malfunctions[randomIndex];
            malfunctionCompleteTimer = currentMalfunction.timeToSolve;

            ActivateIndicators();
        }
    }

    private void UpdateMalfunction()
    {
        // Add recognition time to score, so how fast the player reacted to the current situation
        if(currentSolutionNeeded == 0)
        {
            playerScore.malfunctionRecognitionTime += currentMalfunction.timeToSolve - malfunctionCompleteTimer;
        }

        currentSolutionNeeded++;

        // If current solution doesn't exist, then we know the malfunction was succesfully completed
        if (currentSolutionNeeded >= currentMalfunction.solutions.Count)
        {
            Debug.Log("Malfunction completed!");
            playerScore.malfunctionsSolved++;
            correctSound.Play();
            ResetMalfunction();
        }
        else
        {
            // If there is a next solution, then update the delay for the next solution
            currentSolutionDelay = currentMalfunction.solutions[currentSolutionNeeded].delay;
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

        // Reset indicators
        foreach (NaphthaCrackerPart part in parts)
        {
            part.ResetAllElements();
        }

        // Reset slider values
        pump.value = (int)LevelState.Middle;
        preheater.value = (int)LevelState.Middle;

        SetMalfunctionTimer();
    }

    private void UpdateMalfunctionTimers()
    {
        // Make sure the user waits the given solution delay before giving the solution
        if (currentSolutionDelay > 0f)
        {
            currentSolutionDelay -= Time.deltaTime;
        }

        // Make sure the user doesn't take too much time solving the malfunction
        if (malfunctionCompleteTimer > 0f)
        {
            malfunctionCompleteTimer -= Time.deltaTime;
        }
        else
        {
            // Show result screen when user took too long to solve the malfunction
            ShowResultScreen();
        }
    }

    /// <summary>
    /// Makes the waiting time for the next malfunction smaller based on the given amount
    /// </summary>
    /// <param name="amount"> Amount of waiting time in seconds </param>
    private void SpeedUpToNextMalfunction(float amount)
    {
        malfunctionTimer -= amount;
    }
    #endregion // MalfunctionLogic

    #region SolutionChecking
    public void SliderSolution(float value)
    {
        // If the user uses a slider when there is no malfunction,
        // punish him by giving him the next malfunction faster
        if (!currentMalfunction)
        {
            SpeedUpToNextMalfunction(1f);
            return;
        }

        // Check if the solution is right
        int iValue = (int)value;
        Malfunction.Solution currentSolution = currentMalfunction.solutions[currentSolutionNeeded];
        if(currentSolution.controller == usedController
            && (int)currentSolution.state == iValue
            && currentSolutionDelay <= 0f)
        {
            UpdateMalfunction();
        }
        else
        {
            // Show result screen when user got the wrong solution
            ShowResultScreen();
        }
    }

    public void ButtonSolution()
    {
        // If the user uses a button when there is no malfunction,
        // punish him by giving him the next malfunction faster
        if (!currentMalfunction)
        {
            SpeedUpToNextMalfunction(1f);
            return;
        }

        // Check if the solution is right
        Malfunction.Solution currentSolution = currentMalfunction.solutions[currentSolutionNeeded];
        if (currentSolution.controller == usedController
            && currentSolution.state == LevelState.Middle
            && currentSolutionDelay <= 0f)
        {
            UpdateMalfunction();
        }
        else
        {
            // Show result screen when user got the wrong solution
            ShowResultScreen();
        }
    }
    #endregion // SolutionChecking

    private void ShowResultScreen()
    {
        resultScreen.ShowResult(currentMalfunction, playerScore);
        wrongSound.Play();
        this.enabled = false; // Make sure "Show Result method is called only once"
    }

    #region Setters
    public void SetUsedController(int controllerIndex)
    {
        usedController = (Controllers)controllerIndex;
    }

    private void SetMalfunctionTimer()
    {
        malfunctionTimer = UnityEngine.Random.Range(malfunctionWaitingTime.x, malfunctionWaitingTime.y);
    }
    #endregion // Setters
}
