using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class BaseGhost : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactText;
    private List<NPCConversation> conversations;
    [SerializeField] private Transform player;

    private void Awake() {
        UILanguageUpdater languageUpdater = Object.FindFirstObjectByType<UILanguageUpdater>();
        if (languageUpdater != null) {
            conversations = languageUpdater.ConversationGhostManager();
        }
    }

    public void Interact(Transform interactorTranform) {
        transform.LookAt(player);
        ConversationManager.Instance.StartConversation(conversations[0]);
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void DeleteListElement() {
        if (conversations.Count > 1) {
            conversations.RemoveAt(0);
        }
    }
}
