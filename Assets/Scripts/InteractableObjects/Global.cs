using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{

    [SerializeField] private List<GameObject> interactableList = new List<GameObject>();
    public GameObject InteractUI;
    public PlayerController playerController;


    private void Start() {
        InitializeList();
    }
    // Update is called once per frame
    private void Update() {
        CheckActive();
        MouseActive();
    }

    private void InitializeList() {
        foreach(GameObject interactable in interactableList) {
            interactable.SetActive(false);
        }
    }

    private void CheckActive() {
        bool anyActive = false;
        foreach(GameObject interactable in interactableList) {
            if (interactable.activeSelf) {
                anyActive = true;
                break;
            }
        }
        InteractUI.SetActive(!anyActive);
    }

    private void MouseActive() {
        if (!InteractUI.activeSelf) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerController.DisableLookInput();
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.EnableLookInput();
        }
    }
}
