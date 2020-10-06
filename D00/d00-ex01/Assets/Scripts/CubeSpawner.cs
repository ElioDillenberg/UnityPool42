using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    public GameObject aGameObj;
    public GameObject sGameObj;
    public GameObject dGameObj;

    private Cube A;
    private Cube S;
    private Cube D;

    private Cube[] Cubes;

    public float spawnTime = 10000;
    private float nextSpawn = 0;


    bool spawnRandomCube() {
        int random = Random.Range(0, 3);

        return Cubes[random].instantiate();
    }

    void toSpawndOrNotToSpawn() {
        if (nextSpawn == 0) {
            if (spawnRandomCube()) {
                nextSpawn = spawnTime;
            }
        } else {
            nextSpawn--;
        }
    }

    void toDestroyOrNotToDestroyCubes() {
        foreach(Cube cube in Cubes) {
            if(Input.GetKeyDown(cube.key)) {
                cube.destroyInstance();
            }
            // cube.checkYAxisDestroy();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cubes = new Cube[3];
        Cubes[0] = aGameObj;
        //  new Cube(aGameObj, "a");
        Cubes[1] = sGameObj;
        Cubes[2] = dGameObj;
        // Cubes[1] = new Cube(sGameObj, "s");
        // Cubes[2] = new Cube(dGameObj, "d");
    }

    // Update is called once per frame
    void Update()
    {
        toSpawndOrNotToSpawn();
        toDestroyOrNotToDestroyCubes();

    }
}

//MISSING --> Counting scores!