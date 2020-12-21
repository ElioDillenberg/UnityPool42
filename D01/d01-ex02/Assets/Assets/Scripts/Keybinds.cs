using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    private GameObject currentKey = null;

    private void OnGUI() {
        if (currentKey != null) {
            Event e = Event.current;
            if (e.isKey) {
                GameManager.instance.keyBinds[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clickedKey) {
        currentKey = clickedKey;
    }

    void Start() {
        // set keybind text on scene load
        foreach(KeyValuePair<string, KeyCode> entry in GameManager.instance.keyBinds) {
            transform.Find(entry.Key).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = entry.Value.ToString();
        }
    }
}
