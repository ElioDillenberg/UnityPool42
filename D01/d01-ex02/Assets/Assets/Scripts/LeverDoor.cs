using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour
{
    public GameObject Stick;
    public Door Door;

    public void Trigger() {
        if (Door.frozen) {
            Stick.transform.eulerAngles = new Vector3(
                Stick.transform.eulerAngles.x,
                Stick.transform.eulerAngles.y,
                -Stick.transform.eulerAngles.z
            );
            Door.frozen = !Door.frozen;
        }
    }
}
