using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndQuit : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject characterChoice;

    public void StartGame() {
        MainMenu.SetActive(false);
        characterChoice.SetActive(true);
    }

    public void ReturnMenu() {
        characterChoice.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
