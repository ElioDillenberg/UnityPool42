using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public string playerTag;
    public GameObject gameOverCanvas;

    public void DeclareWinner(string winnerTag) {
        Debug.Log(playerTag == winnerTag ? "Defeat" : "Victory");
        GetComponent<UnityEngine.UI.Text>().text = playerTag == winnerTag ? "Defeat" : "Victory";
        gameOverCanvas.SetActive(true);
    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
