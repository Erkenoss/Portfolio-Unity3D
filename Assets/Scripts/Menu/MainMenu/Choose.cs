using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Choose : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject background;
    private void Awake() {
        background.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData) {
        background.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData) {
        background.SetActive(false);
    }
}
