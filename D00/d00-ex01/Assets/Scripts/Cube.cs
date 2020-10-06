using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float baseMovementSpeed = 3f;
    private float movementSpeed;

    private Vector3 direction;
    public float yMin;

    private GameObject  gameObj;
    private GameObject  instance;
    public  GameObject  target;
    private bool        exists = false;
    public string      key;

    public Cube(GameObject gameObjInput, string keyInput) {
        gameObj = gameObjInput;
        key = keyInput;
    }

    public bool instantiate() {
        if (!exists) {
            instance = Instantiate(gameObj);
            exists = true;
            return true;
        }
        return false;
    }

    public void destroyInstance() {
        Destroy(instance);
        exists = false;
    }

    // public void checkYAxisDestroy() {
    //     transform.Translate(direction * Time.deltaTime * movementSpeed, Space.World);
    //     if (transform.position.y < yMin) {
    //         Destroy(instance);
    //         exists = false;
    //     }
    // }

    void CubeMovement() {
        // if (transform.position.y < yMin) {
            // exists = false;
            // this.destroyInstance();
        // }
        transform.Translate(direction * Time.deltaTime * movementSpeed, Space.World);
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = baseMovementSpeed + Random.Range(0f, 7.0f);
        direction = new Vector3(0.35f, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // moves cube down at random speed + base speed;
        CubeMovement();
        Debug.Log(transform.position.y - target.transform.position.y);
    }
}
