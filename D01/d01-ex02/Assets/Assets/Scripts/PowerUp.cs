// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // information about the power up (to be used by player)
    public float speed;
    public float jumpForce;
    public float timer;

    // handling respawn of power up (to be used by power up)
    [HideInInspector]
    public bool onCooldown;
    public float respawnTimer;
    private float timeToRespawn;

    void Start() {
        onCooldown = false;
        timeToRespawn = 0;
    }
    
    public void Use() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        onCooldown = true;
    }

    // non-physics related update!
    void Update() {
        if (onCooldown) {
            timeToRespawn += Time.deltaTime;
            if (timeToRespawn >= respawnTimer) {
                onCooldown = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<Collider2D>().enabled = true;
                timeToRespawn = 0;
            }
        }
    }
}
