using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public bool reachedFinish = false;
    public GameObject playerHeart;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == playerHeart) {
            reachedFinish = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == playerHeart) {
            reachedFinish = false;
        }
    }
}
