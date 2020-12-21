using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public KeyCode jump, left, right, reset, selectRed, selectBlue, selectYellow, use;
    public Dictionary<string, KeyCode> keyBinds;

    private void Awake() {
        keyBinds = new Dictionary<string, KeyCode>() {
            { "Left", left },
            { "Right", right },
            { "Jump", jump },
            { "Reset", reset },
            { "Select red", selectRed },
            { "Select blue", selectBlue },
            { "Select yellow", selectYellow },
            { "Use", use }
        };
    }

    private void OnGUI() {
        // if (currentKey != null) {
            var hello = Event.current.keyCode;
            Debug.Log(hello);

        // }        
    }

    private void Start() {

    }

    private void Update() {

    }
}