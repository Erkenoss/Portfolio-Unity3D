using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Languages : MonoBehaviour, IPointerDownHandler
{
    public GameObject fr;
    public GameObject en;
    private bool mouseDown;

    private void Awake() {
        mouseDown = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        mouseDown = !mouseDown;
        if (mouseDown) {
            fr.SetActive(true);
            en.SetActive(true);
        }
        else {
            fr.SetActive(false);
            en.SetActive(false);
        }
    }
}
