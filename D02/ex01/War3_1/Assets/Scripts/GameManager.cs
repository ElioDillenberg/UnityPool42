using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameOver gameOver;
    // public AudioManager audioManager;

    void Awake() {
        // Instantiate(audioManager);
    }

    public void EndOfGame(string winnerTag){
        Debug.Log("IN END OF GAME!");
        gameOver.DeclareWinner(winnerTag);
        Time.timeScale = 0;
    }


    // public void GameOver(string loserTag) {
        // Debug.Log(loserTag + "have lost!");
    // }
}
