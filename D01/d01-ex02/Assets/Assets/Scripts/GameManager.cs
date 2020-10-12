using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Controls controls;

    public FinishTrigger[] FinishTriggers;
    public GameObject gameWonCanvas;
    public int level;

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void NextLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(level + 1);
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
