using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Markus Schwalb
/// script to handle Menu Buttons 
/// </summary>
public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Restarts/loads the Game Level i (buildmanager index)
    /// </summary>
    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);

    }
    

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
