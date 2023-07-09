using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{   
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktop()
    {
        Application.Quit();

    }

}
