using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml;

public class LanguageManagerMenu : MonoBehaviour
{
    public static LanguageManagerMenu Instance;
    public TextAsset dictionary;
    public int currentLanguageIndex;
    public int newLanguageIndex;
    public List<TextMeshProUGUI> uiTexts;

    private List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    private Dictionary<string, string> currentLanguage;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguages();
            SetLanguage(currentLanguageIndex);
            UpdateUIText();
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (newLanguageIndex != currentLanguageIndex) {
            SetLanguage(newLanguageIndex);
            currentLanguageIndex = newLanguageIndex;
            UpdateUIText();
        }
    }

    private void SetLanguage(int index) {
        if (index >= 0 && index < languages.Count) {
            currentLanguage = languages[index];
        }
    }

    public void ChangeLanguage(int index) {
        if (index >= 0 && index < languages.Count) {
            newLanguageIndex = index;
        }
    }

    private void LoadLanguages() {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);

        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languageList) {
            XmlNodeList languageContent = languageValue.ChildNodes;

            Dictionary<string, string> lang = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent) {
                if (value.NodeType == XmlNodeType.Element) {
                    lang.Add(value.Name, value.InnerText);
                }
            }

            languages.Add(lang);
        }
    }

    private void UpdateUIText() {
        foreach (TextMeshProUGUI uiText in uiTexts) {
            if (currentLanguage.TryGetValue(uiText.name, out string translatedText)) {
                uiText.text = translatedText;
            }
        }
    }
}
