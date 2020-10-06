using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Controls controls;

    public FinishTrigger[] FinishTriggers;
    public GameObject gameWonCanvas;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    private void Restart() {
        SceneManager.LoadSceneAsync(0);
    }

    private bool allPlayersReachedFinish() {
        foreach(FinishTrigger finishTrigger in FinishTriggers) {
            if (!finishTrigger.reachedFinish) {
                return false;
            }
        }
        return true;
    }

    private void checkFinish() {
        if (allPlayersReachedFinish()) {
            // Debug.Log("YOU WON!");
            gameWonCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkFinish();
        if (Input.GetKey(controls.reset)) {
            Restart();
        }
    }
}
