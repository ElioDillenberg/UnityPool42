using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public GameObject balloon;

    private bool isAlive;
    public float breath = 100;
    public float inputFrameRate = 30;
    public float frameRate;
    private float balloonHealth = 100;
    public float breathCost = 40f;
    public float balloonHealthCost = 10;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        frameRate = inputFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            if (balloonHealth <= 0) {
                isAlive = false;
                Destroy(balloon);
                Debug.Log("Balloon life time " + Mathf.RoundToInt(Time.time) + " seconds!");
            } else {
                if (Input.GetKeyDown("space") && breath >= breathCost) {
                    //handle balloon
                    balloonHealth -= balloonHealthCost;
                    balloon.transform.localScale += new Vector3(0.10f, 0.10f, 0.10f);
                    //handle breath
                    breath -= breathCost;
                }
            }
        }

        // breath back and balloon depletion every X framerate
        if (frameRate > 0) {
            frameRate--;
        } else {
            if (breath <= 90) {
                breath += 10;
            }   
            if (balloonHealth < 100) {
                balloonHealth++;
                balloon.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            frameRate = inputFrameRate;
        }
    }
}
