using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform TelePos;

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, TelePos.position);
    }
}
