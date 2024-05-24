using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    private List<NPCConversation> conversations;

    private void Awake() {
        UILanguageUpdater languageUpdater = Object.FindFirstObjectByType<UILanguageUpdater>();
        if (languageUpdater != null) {
            conversations = languageUpdater.ConversationGhostManager();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)) {
            ConversationManager.Instance.StartConversation(conversations[0]);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (conversations.Count > 1) {
            conversations.RemoveAt(0);
        }
    }
}
