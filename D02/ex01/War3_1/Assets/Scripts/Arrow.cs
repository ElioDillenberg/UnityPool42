using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Unit target;
    // public Transform pos1;
    public float lifeTime;
    public float speed;
    private Vector3 direction;
    public float damage;

    void Start()
    {
        // transform.position = pos1.position;
        Destroy(gameObject, lifeTime);
        direction = target.transform.position - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == target) {
            Unit unit = other.gameObject.GetComponent<Unit>();
            unit.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized * 0.1f);
        transform.position += direction * speed * Time.deltaTime;
    }
}
