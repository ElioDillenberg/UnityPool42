using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public Transform pos1;
    public float lifeTime;
    public float speed;
    private Vector3 direction;

    void Start()
    {
        transform.position = pos1.position;
        Destroy(gameObject, lifeTime);
        direction = target.transform.position - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == target) {
            Player player = other.gameObject.GetComponent<Player>();
            player.Die();
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized * 0.1f);
        if (hit) {
            if (hit.collider.tag == "MovingPlatform" || hit.collider.tag == "Untagged") {
                Destroy(gameObject);
            }
        }
        transform.position += direction * speed * Time.deltaTime;
    }
}
