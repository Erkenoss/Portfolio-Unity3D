using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLink : MonoBehaviour
{
    public string Github;
    public string Linkedin;
    public string MBTI;
    public string Portfolio;
    public string CrimsonRise;
    public string RBNB;
    public string Shell;

    public void OpenURLGithub() {
        Application.OpenURL(Github);
    }
    public void OpenURLLinkedin() {
        Application.OpenURL(Linkedin);
    }
    public void OpenURLMBTI() {
        Application.OpenURL(MBTI);
    }
    public void OpenURLPortfolio() {
        Application.OpenURL(Portfolio);
    }
    public void OpenURLCrimson() {
        Application.OpenURL(CrimsonRise);
    }
    public void OpenURLRBNB() {
        Application.OpenURL(RBNB);
    }
    public void OpenURLShell() {
        Application.OpenURL(Shell);
    }
}
