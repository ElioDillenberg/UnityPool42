using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public Transform startPos;
    public float speed;
    private Vector3 nextPos;
    public bool frozen = false;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position == pos1.position){
            nextPos = pos2.position;
        } else if (transform.position == pos2.position){
            nextPos = pos1.position;
        }

        if (!frozen) {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
