using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector2 target;
    public float speed;
    private Animator animator;
    private Vector3 oldPos;
    public bool isRunning;
    private Vector2 prevDirection;
    private AudioManager audioManager;
    public string[] acknowledgeSounds;
    public string spawnSound;
    private GameObject selectedGameObject;
    public float healthPool;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        prevDirection = new Vector2(0f, -1f);
        target = transform.position;
        oldPos = transform.position;
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
        animator.SetFloat("movingDirection", 4f);
        audioManager.Play(spawnSound);
        selectedGameObject = transform.Find("Selected").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // detected by turret
        Debug.Log("DETECTED!");
        Tower tower = other.gameObject.GetComponent<Tower>();
        if (tower != null) {
            tower.targetAcquired = true;
            tower.target = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // leaving turret range
        Tower tower = other.gameObject.GetComponent<Tower>();
        if (tower != null) {
            tower.targetAcquired = false;
            tower.target = null;
        }
    }

    
    public void setSelectedVisible(bool visible) {
        selectedGameObject.SetActive(visible);
    }

    private int DirectionToIndex(Vector2 _direction) {
        Vector2 norDir = _direction.normalized;

        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;

        if (angle < 0) {
            angle += 360; //normalize angles --> -45 becomes 325 to create proper rotation
        }

        float stepCount = angle / step;
        return (int)stepCount;
    }

    public void moveToPosition(Vector3 newPosition) {
        // define new position to move to in Update
        target = newPosition;
        // take a sound at random and plays it
        int nbSounds = acknowledgeSounds.Length;
        System.Random random = new System.Random();
        int soundIndex = random.Next(nbSounds);
        audioManager.Play(acknowledgeSounds[soundIndex]);
    }

    public void TakeDamage(float dmg) {
        healthPool -= dmg;
        if(healthPool <= 0) {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

        Vector2 direction = target - (Vector2)transform.position;
        if (direction.magnitude >= 0.01f) {
            //unit is moving, apply moving animation
            prevDirection = direction;
            int movingAnimationIndex = DirectionToIndex(direction);
            animator.SetBool("isMoving", true);
            animator.SetFloat("movingDirection", (float)movingAnimationIndex);
        } else {
            //unit is not moving, apply idle animation
            animator.SetBool("isMoving", false);
        }
    }
}
