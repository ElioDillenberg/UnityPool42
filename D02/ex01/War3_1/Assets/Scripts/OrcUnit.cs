using System;
using UnityEngine;

public class OrcUnit : Unit
{
    private TownHall ennemyTownHall;

    protected override void Start() {
        base.Start();
        // rush ennemy town hall
        ennemyTownHall = GameObject.Find(ennemyTag + "TownHall").GetComponent<TownHall>();
        attackEnnemy(ennemyTownHall);
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (other.tag == ennemyTag && other.gameObject.GetComponent<HumanUnit>()) {
            HumanUnit humanUnit = other.gameObject.GetComponent<HumanUnit>();
            attackEnnemy(humanUnit);
        }
    }

    protected override void Update() {
        // if target dies, focus back on the ennemy TownHall
        if (!ennemyTarget) {
            attackEnnemy(ennemyTownHall);
        }
        base.Update();
    }
}
