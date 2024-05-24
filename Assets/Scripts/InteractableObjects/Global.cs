using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{

    [SerializeField] private List<GameObject> interactableList = new List<GameObject>();
    public GameObject InteractUI;


    private void Start() {
        InitializeList();
    }
    // Update is called once per frame
    private void Update() {
        CheckActive();
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
}
