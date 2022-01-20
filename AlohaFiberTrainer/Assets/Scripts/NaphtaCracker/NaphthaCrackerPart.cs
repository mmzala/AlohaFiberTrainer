using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelState
{
    Low,
    Middle,
    High,
}

public enum ElementType
{
    Volume,
    Temperature,
    Pressure,
    Q,
}

public class NaphthaCrackerPart : MonoBehaviour
{
    [SerializeField]
    private Image[] elements;
    
    public Color[] stateColors = { new Color(245, 255, 73)
                                 , new Color(171, 2555, 136)
                                 , new Color(255, 57, 50) };

    private LevelState[] elementStates;

    private void Awake()
    {
        // Init element states to middle state (all elements start at middle level)
        elementStates = new LevelState[elements.Length];
        for (int i = 0; i < elementStates.Length; i++)
        {
            elementStates[i] = LevelState.Middle;
        }
    }

    /// <summary>
    /// Changes the state of chosen element and changes the color of it
    /// </summary>
    /// <param name="element"> What element you want to change </param>
    /// <param name="state"> To what state you want to change it to </param>
    public void ChangeState(ElementType element, LevelState state)
    {
        // Make sure the element exists
        int elementNum = (int)element;
        if (elementNum >= elements.Length) return;

        // Change state and color of the element
        elementStates[elementNum] = state;
        elements[elementNum].color = stateColors[(int)state];
    }

    public void ResetAllElements()
    {
        int defaultStateIndex = (int)LevelState.Middle;
        for (int i = 0; i < elements.Length; i++)
        {
            elementStates[i] = LevelState.Middle;
            elements[i].color = stateColors[defaultStateIndex];
        }
    }
}
