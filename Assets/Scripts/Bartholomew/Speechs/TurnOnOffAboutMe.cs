using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TurnOnOffAboutMe : MonoBehaviour
{
    public Light mylight;
    private int waitOn = 2;

    public void ToggleLight() {
        StartCoroutine(TurnOn(waitOn));
    }

    private IEnumerator TurnOn(int waitOn) {

        yield return new WaitForSeconds(waitOn);

        if (mylight != null) {
            mylight.enabled = !mylight.enabled;
        }

    }
}