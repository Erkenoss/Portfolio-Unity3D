using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobbies : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;
    public GameObject PlayerInteractUI;
    public GameObject panel;

    private void Awake() {
        panel.SetActive(false);
    }
    public void Interact(Transform interactorTranform) {
        if (panel.activeSelf) {
            panel.SetActive(false);
            PlayerInteractUI.SetActive(true);
        }
        else {
            panel.SetActive(true);
            PlayerInteractUI.SetActive(false);
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
