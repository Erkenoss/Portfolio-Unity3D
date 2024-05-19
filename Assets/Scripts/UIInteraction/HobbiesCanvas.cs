using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobbiesCanvas : MonoBehaviour
{
    [SerializeField] public List<Transform> hobbies = new List<Transform>();
    [SerializeField] public GameObject hobbiesPanel;
    private int indexList;

    private void Awake() {
        listHobbies();
        indexList = 0;
    }
    public void Next() {
        hobbies[indexList].gameObject.SetActive(false);
        indexList ++;
        hobbies[indexList].gameObject.SetActive(true);
    }

    public void Previous() {
        hobbies[indexList].gameObject.SetActive(false);
        indexList --;
        hobbies[indexList].gameObject.SetActive(true);
    }

    private void listHobbies() {
        if (hobbiesPanel != null) {

            hobbies.Clear();
            int index = 0;

            foreach(Transform t in hobbiesPanel.transform) {

                if (index >= 2) {
                    hobbies.Add(t);
                }

                index ++;
            }
        }
    }
}
