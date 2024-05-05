using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGhost : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;
    private FindWayPoints way;

    private void Awake() {
        way = GetComponent<FindWayPoints>();
    }

    public void Interact(Transform interactorTranform) {
        Debug.Log("hey, moi c'est Bartholomew");
        way.Move();
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
