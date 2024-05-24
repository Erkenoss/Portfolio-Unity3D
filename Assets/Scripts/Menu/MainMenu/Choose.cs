using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class Choose : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public new string name;
    public TextMeshProUGUI textMeshPro;
    public void OnPointerEnter(PointerEventData eventData) {
        textMeshPro.text = name;
    }
    public void OnPointerExit(PointerEventData eventData) {
        textMeshPro.text = "";
    }

    public void OnPointerDown(PointerEventData eventData) {
        SceneManager.LoadScene("MainScene");
    }
}
