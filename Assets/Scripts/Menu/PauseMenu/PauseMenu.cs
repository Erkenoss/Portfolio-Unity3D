using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isActive;

    private void Awake() {
        pauseMenu.SetActive(false);
    }

    private void Update() {
        TogglePauseMenu();
    }

    private void TogglePauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isActive = !isActive;
            pauseMenu.SetActive(isActive);
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
