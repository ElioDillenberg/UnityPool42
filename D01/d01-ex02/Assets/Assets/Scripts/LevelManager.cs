using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public FinishTrigger[] finishTriggers;
    public GameObject[] players;
    public GameObject gameWonCanvas;
    public GameObject gameOverCanvas;
    private float timer = 0;
    private int level;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.instance;
        Time.timeScale = 1;
        level = SceneManager.GetActiveScene().buildIndex;
    }

    public void Restart() {
        SceneManager.LoadScene(level);
    }

    public void NextLevel() {
        SceneManager.LoadScene(level + 1);
    }

    private bool AllPlayersReachedFinish() {
        foreach(FinishTrigger finishTrigger in finishTriggers) {
            if (!finishTrigger.reachedFinish) {
                return false;
            }
        }
        return true;
    }

    private void CheckFinish() {
        if (AllPlayersReachedFinish()) {
            Score.score = timer;
            gameWonCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private bool PlayerDead() {
        foreach(GameObject player in players) {
            if (!player)
                return true;
        }
        return false;
    }

    private void CheckPlayerDead(){
        if (PlayerDead()){
            Score.score = timer;
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void PlayerInput() {
        if (Input.GetKey(gameManager.keyBinds["Reset"])) {
            Restart();
        }
        if (Input.GetKey(gameManager.keyBinds["Menu"])) {
            OpenMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckFinish();
        CheckPlayerDead();
        PlayerInput();
        timer += Time.deltaTime;
    }

    public void OpenMenu() {
        SceneManager.LoadScene(0);
    }
}
