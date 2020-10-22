using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerButton : MonoBehaviour
{
    public GameObject Platform;

    // tool to convert layerMask into layerNumber
    private int getLayerNumber(LayerMask layerMask){
        int layerNumber = 0;
        while(layerMask > 0) {
            layerMask = layerMask >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }

    // change 
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // change layer
            Player player = other.gameObject.GetComponent<Player>();
            LayerMask layerMask = player.canStandOn;
            int layerNumber = getLayerNumber(layerMask);
            Platform.layer = layerNumber;
            // change colors
            SpriteRenderer spriteRendererPlatform = Platform.gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRendererButton = gameObject.GetComponent<SpriteRenderer>();
            spriteRendererPlatform.color = player.canStandOnColor;
            spriteRendererButton.color = player.canStandOnColor;
        }
        
    }
}
