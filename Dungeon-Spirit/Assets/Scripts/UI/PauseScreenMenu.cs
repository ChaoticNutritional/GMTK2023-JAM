using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenMenu : MonoBehaviour {
    
    public static bool GameIsPause = false;
    public GameObject pauseScreenMenu;

    void Awake()
    {   
        if (pauseScreenMenu == null)
            pauseScreenMenu = gameObject;
        
    }


    private void Start()
    {
        DS_SceneManager.instance.pauseScreenMenu = this;
        TogglePauseScreen();
    }

    public void TogglePauseScreen()
    {
        pauseScreenMenu.SetActive(!pauseScreenMenu.activeInHierarchy);
        GameIsPause = pauseScreenMenu.activeInHierarchy;
    }

    public void Resume ()
    {
        pauseScreenMenu.SetActive(false);
        GameIsPause = false;

    }

    public void RestartRoom()
    { 
    
    }

    public void QuitToMainMenu()
    {

    }    

    public void QuitToDesktop()
    {
            Application.Quit();
    }
}
