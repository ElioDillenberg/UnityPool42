using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
    public Unit unit;
    public float spawnRate;
    private float spawnTimer = 0f;
    public Vector3 unitSpawnLocation;

    void SpawnNewUnit() {
        Instantiate(unit, unitSpawnLocation, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        unitSpawnLocation = transform.position + new Vector3(1.2f, -1.2f);
        SpawnNewUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= spawnRate) {
            SpawnNewUnit();
            Vector3 spawnLocation = transform.position + new Vector3(1.2f, -1.2f);
            Unit newUnit = Instantiate(unit, spawnLocation, Quaternion.identity);
            spawnTimer = 0f;
        } else {
            spawnTimer += Time.deltaTime;
        }
    }
}
