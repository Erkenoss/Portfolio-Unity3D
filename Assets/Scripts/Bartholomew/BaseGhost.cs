using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGhost : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;

    public void Interact(Transform interactorTranform) {
        Debug.Log("hey, moi c'est Bartholomew");
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
