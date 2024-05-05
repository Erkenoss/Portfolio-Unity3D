using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutMe : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;

    public void Interact(Transform interactorTranform) {
        Debug.Log("Coucou, tu es sur AboutMe");
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
