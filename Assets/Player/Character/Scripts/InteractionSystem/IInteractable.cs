using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {

    // (Position) Transform of object interactable
    // (Text) TextMeshPro in the UI
    // (Interaction) What is the fuck when we interact with?

    Transform GetTransform();
    string GetInteractText();
    void Interact(Transform interactorTransform);

}
