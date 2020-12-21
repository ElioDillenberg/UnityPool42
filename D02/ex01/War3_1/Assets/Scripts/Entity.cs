using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float healthPool;

    public float offsetMeleeRangeForAttackers;

    public virtual void TakeDamage(float dmg) {
        healthPool -= dmg;
        if (healthPool <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject); 
    }
    // Start is called before the first frame update
    // void Start()
    // {
        // 
    // }

    // Update is called once per frame
    // void Update()
    // {
        // 
    // }
}
