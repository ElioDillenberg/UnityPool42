using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBuilding : Building
{
    public TownHall TownHall;

    protected override void Die() {
        TownHall.spawnRate += 2.5f;
        base.Die();
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
