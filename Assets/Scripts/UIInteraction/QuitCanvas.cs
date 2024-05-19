using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCanvas : MonoBehaviour
{
    public Transform panel;
    public GameObject PlayerInteractUI;
    private void Awake() {
        panel = transform.parent;
    }

    public void Quit() {
        if (panel != null) {
            panel.gameObject.SetActive(false);
            PlayerInteractUI.SetActive(true);
        }
    }
}
