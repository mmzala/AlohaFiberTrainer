using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [Tooltip("This text will come before the time")]
    public string defaultText = "Time: ";

    private void Update()
    {
        System.TimeSpan timeSpan = GetCurrentTime();

        string timer = timeSpan.Hours.ToString("00") + ":" +
                       timeSpan.Minutes.ToString("00") + ":" +
                       timeSpan.Seconds.ToString("00");

        text.text = defaultText + timer;
    }

    public System.TimeSpan GetCurrentTime()
    {
        return System.DateTime.Now.TimeOfDay;
    }
}
