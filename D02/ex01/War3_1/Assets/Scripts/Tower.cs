using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : RegularBuilding
{
    // transform that will define instantiation position for arrows fired
    public Transform initArrow;

    // arrow prefab (dmg dealt is based on arrow, not on turret)
    public Arrow arrow;

    // floats used to calculate fire rate
    public float shotsPerMinute;
    private float timer;

    // enter the ennemy tag exactly to transfer to arrow when instantiated
    public string ennemyTag;

    // handle all targets in range + current target
    [HideInInspector]
    public List<GameObject> targets;
    [HideInInspector]
    public GameObject currentTarget;

    // shooting direction
    private Vector3 direction;
    // fixed offset to rotate the arrows in the right direction
    private float projectileOffset = -90;

    void Shoot() {
        // shoot shotsPerMinute arrow every 60 seconds
        timer += Time.deltaTime;
        if (timer / (60 / shotsPerMinute) > 1) {
            // calculate arrow shooting direction
            Vector3 difference = currentTarget.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            // instantiate new arrow and set its target + ennemyTag 
            Arrow insArrow = Instantiate(arrow, initArrow.transform.position, Quaternion.Euler(0f, 0f, rotZ + projectileOffset));
            insArrow.ennemyTag = ennemyTag;
            insArrow.target = currentTarget;
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentTarget) {
            // select a new currentTarget from the targets in range (logic -> first in range, first shot)
            if (targets.Count > 0) {
                currentTarget = targets[0];
            }
        } else {
            // shoot at currentTarget
            direction = currentTarget.transform.position - transform.position;
            Shoot();
        }
    }
}
