using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float gravity = 9.81f;
    private float jumpForce = 450f;

    // public GameObject explosion;

    private void Locomotion() {
        if (Input.GetKeyDown("space")) {
            movement.y = jumpForce * Time.deltaTime;
        }
        else
            movement.y -= gravity * Time.deltaTime;
        
        transform.Translate(movement * Time.deltaTime);
    }

    // private IEnumerator Explode() {
    //         GameObject explo = Instantiate(explosion);
    //         explo.transform.position = transform.position;
    //         Destroy(gameObject);
    //         yield return new WaitForSeconds();
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pipe") {
            // Explode();
            gameManager.GameOver();
        } else if (other.tag == "checkPoint") {
            Score.score++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(0, 0);
    }

    // Update is called once per frame
    void  Update()
    {
        Locomotion();
    }
}
