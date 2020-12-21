using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Keybinds
    public KeyCode jump, left, right, reset, selectRed, selectBlue, selectYellow, use, menu;
    public Dictionary<string, KeyCode> keyBinds;

    public static GameManager instance;

    void Awake() {
        if (instance == null) {
            // first awake
            instance = this;
            keyBinds = new Dictionary<string, KeyCode>() {
                { "Left", left },
                { "Right", right },
                { "Jump", jump },
                { "Reset", reset },
                { "Select red", selectRed },
                { "Select blue", selectBlue },
                { "Select yellow", selectYellow },
                // { "Use", use },
                { "Menu", menu }
            };
        } else if (instance != this) {
            // in this case, GameManager already exists, no need for a second one, destroy this object
            Destroy(gameObject);
        }
        // GameManager stays persistent through scenes
        DontDestroyOnLoad(gameObject);
    }
}
