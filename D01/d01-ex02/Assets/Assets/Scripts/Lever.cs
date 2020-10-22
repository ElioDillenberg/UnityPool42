using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Stick;
    public MovingPlatform MovingPlatform;

    public void Trigger() {
        Stick.transform.eulerAngles = new Vector3(
            Stick.transform.eulerAngles.x,
            Stick.transform.eulerAngles.y,
            -Stick.transform.eulerAngles.z
        );
        MovingPlatform.frozen = !MovingPlatform.frozen;
    }
}
