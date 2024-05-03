using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CV : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;

    public void Interact(Transform interactorTranform) {
        Debug.Log("Coucou, tu es sur mon CV");
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
