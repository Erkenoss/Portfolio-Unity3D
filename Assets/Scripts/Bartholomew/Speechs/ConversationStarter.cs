using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private GameObject GhostConversation;
    [SerializeField] private NPCConversation myConversation;

    [SerializeField] private GameObject GhostConversationAfterFirstChoice;
    [SerializeField] private NPCConversation mySecondConversation;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (GhostConversation != null) {
                    ConversationManager.Instance.StartConversation(myConversation);
                }
                else {
                    ConversationManager.Instance.StartConversation(mySecondConversation);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        Destroy(GhostConversation);
    }
}
