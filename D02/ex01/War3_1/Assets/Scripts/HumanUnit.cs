using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HumanUnit : Unit
{
    private GameObject selectedGameObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        selectedGameObject = transform.Find("Selected").gameObject;
    }
    
    public void setSelectedVisible(bool visible) {
        selectedGameObject.SetActive(visible);
    }

    protected override void Die() {
        // remove unit from current selection
        GameRTSController gameRTSController = GameObject.Find("GameRTSController").GetComponent<GameRTSController>();
        gameRTSController.selectedUnits.Remove(this);
        base.Die();
    }
}
