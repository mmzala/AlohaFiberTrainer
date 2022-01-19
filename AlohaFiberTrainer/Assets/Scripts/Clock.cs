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

    public System.TimeSpan startTime { get; private set; }

    private void Start()
    {
        startTime = GetCurrentTime();
    }

    private void Update()
    {
        text.text = defaultText + GetTimerString();
    }

    /// <summary>
    /// Gets played time from start of the training in from of a string
    /// </summary>
    /// <returns> Played time from start of the training </returns>
    public string GetPlayedTime()
    {
        return TimeSpanToString(GetCurrentTime() - startTime);
    }

    /// <summary>
    /// Gets current time in string form
    /// </summary>
    /// <returns> Current time </returns>
    public string GetTimerString()
    {
        return TimeSpanToString(GetCurrentTime());
    }

    public System.TimeSpan GetCurrentTime()
    {
        return System.DateTime.Now.TimeOfDay;
    }

    private string TimeSpanToString(System.TimeSpan timeSpan)
    {
        return timeSpan.Hours.ToString("00") + ":" +
               timeSpan.Minutes.ToString("00") + ":" +
               timeSpan.Seconds.ToString("00");
    }
}
