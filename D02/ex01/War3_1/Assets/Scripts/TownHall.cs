using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building {
    public Unit unit;
    public float spawnRate;
    private float spawnTimer = 0f;
    public Transform spawnLocation;
    public GameManager gameManager;

    void SpawnNewUnit() {
        Instantiate(unit, spawnLocation.transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= spawnRate) {
            SpawnNewUnit();
            spawnTimer = 0f;
        } else {
            spawnTimer += Time.deltaTime;
        }
    }

    override protected void Die() {
        gameManager.EndOfGame(gameObject.tag);
        base.Die();
    }
}
