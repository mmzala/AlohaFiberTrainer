using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Loads the scene based on the given buildIndex
    /// </summary>
    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// Shuts down the running application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
