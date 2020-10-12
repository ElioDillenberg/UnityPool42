using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Controls controls;

    public float xInitialPos;
    public float yInitialPos;
    
    public float jumpForce = 100f;
    public float speed = 10f;
    public float gravity = 9.81f;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Vector2 movement;

    public KeyCode selectKey;
    public GameObject cam;
    public bool selected;

    public GameObject finishCollider;
    public bool reachedFinish;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Entered" + other.name);
       if (other.gameObject == finishCollider) {
           reachedFinish = true;
       }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == finishCollider) {
           reachedFinish = false;
        }
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

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

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
