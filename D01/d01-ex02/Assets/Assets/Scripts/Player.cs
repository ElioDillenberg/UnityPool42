// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Controls controls;
    public LayerMask canStandOn;
    public Color canStandOnColor;

    public float scaleX;
    public float scaleY;

    public float xInitialPos;
    public float yInitialPos;
    
    public float jumpForce = 100f;
    public float speed = 10f;
    public float gravity = 9.81f;

    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Vector2 movement;

    public KeyCode selectKey;
    public GameObject cam;
    public bool selected;

    public GameObject finishCollider;
    public bool reachedFinish;

    private void OnTriggerEnter2D(Collider2D other) {
       if (other.gameObject == finishCollider) {
           reachedFinish = true;
       } else if (other.tag == "Teleporter") {
           Teleporter teleport = other.gameObject.GetComponent<Teleporter>();
           transform.position = teleport.TelePos.position;
       } else if (other.tag == "MovingPlatform") {
           transform.parent = other.transform;
       } else if (other.tag == "Lever") {
           Lever lever = other.gameObject.GetComponent<Lever>();
           lever.Trigger();
       } else if (other.tag == "LeverDoor") {
           LeverDoor lever = other.gameObject.GetComponent<LeverDoor>();
           lever.Trigger();
       } else if (other.tag == "Turret") {
           Turret turret = other.gameObject.GetComponent<Turret>();
           turret.targetAcquired = true;
       }
    }

    public void Die(){
        cam.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == finishCollider) {
           reachedFinish = false;
        } else if (other.tag == "MovingPlatform") {
            transform.parent = null;
        } else if (other.tag == "Turret") {
            Turret turret = other.gameObject.GetComponent<Turret>();
            turret.targetAcquired = false;
        }
    }

    private bool getPlayerInputAction() {
        bool isUsing;
        isUsing = Input.GetKey(controls.use);
        return isUsing;
    }

    private Vector2 getPlayerInputMovement() {
        Vector2 direction = new Vector2(0f, 0f);

        bool up = Input.GetKey(controls.up);
        bool left = Input.GetKey(controls.left);
        bool right = Input.GetKey(controls.right);
        if (right) {
            if (left) {
                direction.x = 0f;
            } else {
                direction. x = 1f;
            }
        } else if (left) {
            direction.x = -1f;
        }

        if (Input.GetKey(controls.up)) {
            direction.y = 1f;
        }
        return direction;
    }

    private void Locomotion() {
        Vector2 playerDirections = getPlayerInputMovement();
        if (selected) {
            movement.x = playerDirections.x * speed;
        } else {
            movement.x = 0f;
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - scaleX / 2, transform.position.y - scaleY / 2), new Vector2(transform.position.x + scaleX / 2, transform.position.y - (scaleY / 2 + 0.01f)), whatIsGround);

        if (selected && isGrounded && playerDirections.y == 1f) {
            movement.y = jumpForce * Time.deltaTime;
        } else if (isGrounded) {
            movement.y = 0;
        } else if (!isGrounded) {
            movement.y -= gravity * Time.deltaTime;
        }

        transform.Translate(movement * Time.deltaTime);
    }

    private void Selection() {
        if (Input.GetKey(selectKey)) {
            selected = true;
            cam.transform.parent = transform;
            Vector3 currentPos = transform.position;
            Vector3 camPos = new Vector3(currentPos.x, currentPos.y, cam.transform.position.z);
            cam.transform.position = camPos;
        } else if (Input.GetKey(controls.char1) || Input.GetKey(controls.char2) || Input.GetKey(controls.char3)) {
            selected = false;
        }
    }

    private void Reset() {
        if (transform.position.y < -25) {
            transform.position = new Vector2(xInitialPos, yInitialPos);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Reset();
        Selection();
        Locomotion();
    }
}
