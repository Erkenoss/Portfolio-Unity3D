using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CV : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;
    public GameObject panel;
    [SerializeField] private Transform player;
    private float distance;

    private void Awake() {
        panel.SetActive(false);
    }

    private void Update() {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance >= 2f && panel.activeSelf) {
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
