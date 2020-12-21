using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    private GameObject currentKey = null;

    public void LoadLevel(int levelToLoad) {
        SceneManager.LoadScene(levelToLoad);
    }

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

    public void PlayerInput() {
        if (currentKey == null) {

        }
    }

    void Start() {
        // set keybind text on scene load
        foreach(KeyValuePair<string, KeyCode> entry in GameManager.instance.keyBinds) {
            // this is ugly
            transform.GetChild(0).Find("Keybinds").Find(entry.Key).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = entry.Value.ToString();
        }
    }
}
