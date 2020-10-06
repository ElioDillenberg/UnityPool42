using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePrefab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void destroy() {
        // Vector2 worldPos = transform.TransformPoint(transform.position);
        // Debug.Log(worldPos);
        // if (worldPos.x < -20f) {
        //     Destroy(gameObject);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        destroy();
    }
}
