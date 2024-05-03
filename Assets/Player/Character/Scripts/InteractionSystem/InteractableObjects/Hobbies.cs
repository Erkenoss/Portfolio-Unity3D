using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobbies : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;

    public void Interact(Transform interactorTranform) {
        Debug.Log("Coucou, tu es sur mes Hobbies");
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
