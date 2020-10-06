using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject gameStartCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    public void GameOver() {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }

    public void Play() {
        Time.timeScale = 1;
        gameStartCanvas.SetActive(false);
    }

    public void Replay() {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
        gameStartCanvas.SetActive(false);
    }
}
