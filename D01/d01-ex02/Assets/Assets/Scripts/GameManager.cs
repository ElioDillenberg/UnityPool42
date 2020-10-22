using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Controls controls;

    public FinishTrigger[] finishTriggers;
    public GameObject[] players;
    public GameObject gameWonCanvas;
    public GameObject gameOverCanvas;
    public int level;
    private float timer = 0;

    public void Restart() {
        timer = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void NextLevel() {
        timer = 0;
        SceneManager.LoadScene(level + 1);
    }

    private bool allPlayersReachedFinish() {
        foreach(FinishTrigger finishTrigger in finishTriggers) {
            if (!finishTrigger.reachedFinish) {
                return false;
            }
        }
        return true;
    }

    private void checkFinish() {
        if (allPlayersReachedFinish()) {
            Score.score = timer;
            gameWonCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private bool playerDead() {
        foreach(GameObject player in players) {
            if (!player)
                return true;
        }
        return false;
    }

    private void checkPlayerDead(){
        if (playerDead()){
            Score.score = timer;
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Start() {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkFinish();
        checkPlayerDead();
        if (Input.GetKey(controls.reset)) {
            Restart();
        }
        timer += Time.deltaTime;
    }
}
