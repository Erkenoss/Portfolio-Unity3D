using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutMe : MonoBehaviour, IInteractable
{
    public string interactText;
    public GameObject panel;
    [SerializeField] private Transform player;
    private float distance;
    private void Awake() {
        panel.SetActive(false);
    }

    private void Start() {
        if (LanguageManager.Instance.frenchLanguage)
        {
            interactText = "A propos de moi";
        }
        else {
            interactText = "About Me";
        }
    }

    private void Update() {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance >= 2 && panel.activeSelf) {
            panel.SetActive(false);
        }
    }

    public void Interact(Transform interactorTranform) {
        if (panel.activeSelf) {
            panel.SetActive(false);
        }
        else {
            panel.SetActive(true);
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
