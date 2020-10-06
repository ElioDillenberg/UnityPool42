using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDecor : MonoBehaviour
{
    public GameObject Pipe;
    public GameObject Ground;

    private float speed = 1f;
    private Vector2 direction;
    public float spawnRate;
    [SerializeField]
    private float nextSpawn;
    public float spawnRateGround;
    [SerializeField]
    private float nextSpawnGround;

    void Awake() {
        spawnNewGround();
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(-1f, 0);
        nextSpawn = spawnRate;
    }

    private float randomPipeHeight() {
        return Random.Range(-1f, 1f);
    }

    private void spawnNewPipe() {
        if (nextSpawn * speed >= spawnRate) {
            GameObject pipe = Instantiate(Pipe);
            Destroy(pipe, 12f / speed);
            pipe.transform.parent = transform;
            float y = randomPipeHeight();
            pipe.transform.position = new Vector2(3.5f, y);
            nextSpawn = 0;
        } else {
            nextSpawn += Time.deltaTime;
        }
    }

    private void toGroundOrNotToGround() {
        if (nextSpawnGround * speed >= spawnRateGround) {
            spawnNewGround();
        } else {
            nextSpawnGround += Time.deltaTime;
        }
    }

    private void spawnNewGround() {
        GameObject ground = Instantiate(Ground);
        Destroy(ground, 15f / speed);
        ground.transform.parent = transform;
        ground.transform.position = new Vector2(2.2f, -4.55f);
        nextSpawnGround = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        speed += 0.0005f * Time.deltaTime;
        spawnNewPipe();
        toGroundOrNotToGround();
    }
}
