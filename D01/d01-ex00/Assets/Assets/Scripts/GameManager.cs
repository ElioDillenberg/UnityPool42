using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Controls controls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Restart() {
        SceneManager.LoadSceneAsync(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(controls.reset)) {
            Restart();
        }
    }
}
