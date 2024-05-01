using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInput input;
    private InputAction interactAction;
    private int layerMask = 1 << 7;
    public IInteractable currentInteract;

    private void Awake() {
        input = GetComponent<PlayerInput>();
        interactAction = input.actions["Interact"];
    }

    private void Update() {
        InteractionRay();
    }

    public void InteractionRay() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5.0f, layerMask)) {
            if (hit.collider.TryGetComponent(out IInteractable interactable)) {
                currentInteract = interactable;
            }
        }
        else {
            currentInteract = null;
        }
        Debug.DrawLine(ray.origin, hit.point, Color.red);
    }

    private void Interact() {
        IInteractable interactable = currentInteract;
        if (interactable != null) {
            interactable.Interact(transform);
        }
    }

    private void OnEnable() {
        interactAction.performed += x => Interact();
    }

    private void OnDisable() {
        interactAction.performed -= x => Interact();
    }
}
