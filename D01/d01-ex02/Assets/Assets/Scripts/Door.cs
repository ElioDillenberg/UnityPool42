using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform posClosed, posOpen;
    public float speed = 5;
    public bool frozen = true;

    private Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = posClosed.position;
    }

    // Update is called once per frame
    void Update() {
        if (!frozen) {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }

        if (transform.position == posClosed.position){
            nextPos = posOpen.position;
            frozen = true;
            } else if (transform.position == posOpen.position){
            nextPos = posClosed.position;
            frozen = true;
        }
    }
        private void OnDrawGizmos() {
            Gizmos.DrawLine(posClosed.position, posOpen.position);
    }
}
