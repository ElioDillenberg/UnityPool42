using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // target of the arrow
    [HideInInspector]
    public GameObject target;

    // layer for Raycasting impact
    public LayerMask whatIsSolid;

    // arrow lifetime and speed -> combined to determine shooting range (not detection range, which is handled with collider)
    public float lifeTime;
    public float speed;

    // shooting direction
    private Vector3 direction;
    public float damage;
    [HideInInspector]
    public string ennemyTag;

    void Start()
    {
        // destory after lifeTime seconds to create range
        Destroy(gameObject, lifeTime);
        direction = (target.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0.1f, whatIsSolid);
        // check for impact with ennemy layer + unit
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag(ennemyTag)) {
                Unit unit = hitInfo.collider.gameObject.GetComponent<Unit>();
                unit.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        transform.position += direction * speed * Time.deltaTime;
    }
}
