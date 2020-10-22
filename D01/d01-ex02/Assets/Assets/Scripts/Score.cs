using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    string resString(){
        string result = "";
        int minutes = 0;
        float seconds = 0f;

        if (score / 60 > 1) {
            minutes = (int)(score / 60);
            result += minutes.ToString() + " minutes and ";
        }
        seconds = score % 60;
        result += seconds.ToString() + " seconds!";
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = resString();
    }
}
