using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;

public class UILanguageUpdater : MonoBehaviour {

    #region Attribute
    public TextMeshProUGUI aboutMeTitle;
    public TextMeshProUGUI aboutMeText;
    public TextMeshProUGUI softskills;
    public TextMeshProUGUI lang;
    public TextMeshProUGUI langTitle;
    public TextMeshProUGUI education1;
    public TextMeshProUGUI education2;
    public TextMeshProUGUI education3;
    public TextMeshProUGUI education4;
    public TextMeshProUGUI experience1;
    public TextMeshProUGUI experience2;
    public TextMeshProUGUI experience3;
    public TextMeshProUGUI experience4;
    public TextMeshProUGUI previous;
    public TextMeshProUGUI previousExp;
    public TextMeshProUGUI chess;
    public TextMeshProUGUI food;
    public TextMeshProUGUI night;
    public TextMeshProUGUI RPG;
    public TextMeshProUGUI concert;
    public TextMeshProUGUI vampire;
    public TextMeshProUGUI lithLi;
    public TextMeshProUGUI pagnaNoise;
    public TextMeshProUGUI BDO;
    public TextMeshProUGUI FFXIV;
    public TextMeshProUGUI nextButton;
    public TextMeshProUGUI nextButton1;
    public TextMeshProUGUI previousButton;
    public TextMeshProUGUI previousButton1;
    public TextMeshProUGUI tutorialMovement;
    public TextMeshProUGUI tutorialCursor;
    public TextMeshProUGUI tutorialGhost;
    public TextMeshProUGUI leaveButton;

    private Dictionary<string, TextMeshProUGUI> textElements;

    //Ghost Conversation
    [SerializeField] private List<NPCConversation> frenchConversation;
    [SerializeField] private List<NPCConversation> englishConversation;

    #endregion

    private void Awake() {
        frenchConversation = new List<NPCConversation>();
        englishConversation = new List<NPCConversation>();
        textElements = new Dictionary<string, TextMeshProUGUI> {
            {"aboutmetitle", aboutMeTitle},
            {"aboutmetext", aboutMeText},
            {"softskills", softskills},
            {"langtitle", langTitle},
            {"lang", lang},
            {"education1", education1},
            {"education2", education2},
            {"education3", education3},
            {"education4", education4},
            {"experience1", experience1},
            {"experience2", experience2},
            {"experience3", experience3},
            {"experience4", experience4},
            {"previous", previous},
            {"previoustext", previousExp},
            {"chess", chess},
            {"food", food},
            {"night", night},
            {"rpg", RPG},
            {"concert", concert},
            {"vampire", vampire},
            {"lithli", lithLi},
            {"pagnanoise", pagnaNoise},
            {"bdo", BDO},
            {"ffxiv", FFXIV},
            {"nextbutton", nextButton},
            {"nextbutton1", nextButton1},
            {"previousbutton", previousButton},
            {"previousbutton1", previousButton1},
            {"tutorialmovement", tutorialMovement},
            {"tutorialcursor", tutorialCursor},
            {"tutorialghost", tutorialGhost},
            {"leavebutton", leaveButton},
        };

        UpdateLanguageUI();
    }

    public List<NPCConversation> ConversationGhostManager() {
        if (LanguageManager.Instance.frenchLanguage) {
            return frenchConversation;
        }
        else {
            return englishConversation;
        }
    }

    public void UpdateLanguageUI() {
        foreach (var entry in textElements) {
            if (LanguageManager.Instance.currentLanguage.TryGetValue(entry.Key, out string value))  {
                entry.Value.text = value;
            }
        }
    }
}
