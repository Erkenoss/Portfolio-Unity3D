using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class LanguageManager : MonoBehaviour {
    public static LanguageManager Instance;

    public TextAsset dictionary;
    public int currentLanguageIndex;
    public Dictionary<string, string> currentLanguage;

    public bool frenchLanguage;

    private List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguages();
            SetLanguage(currentLanguageIndex);
        }
        else {
            Destroy(gameObject);
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
                lang.Add(value.Name, value.InnerText);
            }

            languages.Add(lang);
        }
    }

    public void SetLanguage(int index) {

        if (index == 0) {
            frenchLanguage = true;
        }
        else {
            frenchLanguage = false;
        }

        if (index >= 0 && index < languages.Count) {
            currentLanguageIndex = index;
            currentLanguage = languages[index];
        }
    }
}
