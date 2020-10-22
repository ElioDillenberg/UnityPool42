using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Bullet bullet;
    public float shotsPerMinute;
    private float timer;
    public GameObject target;
    public bool targetAcquired;
    private Vector2 direction;

    void Shoot() {
        // shoot a certain shots per minute!
        timer += Time.deltaTime;
        if (timer / (60 / shotsPerMinute) > 1) {
            Bullet insBullet = Instantiate(bullet);
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetAcquired) {
            direction = (Vector2)target.transform.position - (Vector2)transform.position;
            transform.up = direction;
            Shoot();
        }
    }
}
