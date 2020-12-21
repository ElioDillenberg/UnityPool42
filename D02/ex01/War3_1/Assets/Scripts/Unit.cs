using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{
    private Animator animator;
    // used to display running or idling animation
    public bool isRunning;
    // is unit dead?!
    public bool isDead;

    private AudioManager audioManager;
    // randomly played when moving unit
    public string[] acknowledgeSounds;
    // sound played at instantiation
    public string spawnSound;
    
    public string attackSound;

    public string takeDamageSound;

    public string deathSound;

    // damage dealt to ennemy units and buildings
    public float damage;
    // movement speed
    public float speed;


    // tag considered as ennemy
    public string ennemyTag;
    // position of targeted ennemy
    protected Entity ennemyTarget;
    // how far from the ennemyTarget position can my unit attack it's target?
    private float ennemyTargetAttackRange;
    // is unit attacking/chasing an ennemy unit?
    private bool attacking;
    // is unit in range to attack its target?
    public bool attackingInRange;
    // timer to attack the 
    private float attackTimer = 1f;
    // number of attacks per minute
    public float attackSpeed;

    // position to which unit should move towards
    private Vector2 targetMovingPosition;

    // giving unit order to attack an ennemy unit
    public void attackEnnemy(Entity newEnnemyTarget) {
        ennemyTarget = newEnnemyTarget;
        targetMovingPosition = ennemyTarget.gameObject.transform.position;
        attacking = true;
        audioManager.Play(attackSound);
    }

    public override void TakeDamage(float damage) {
        audioManager.Play(takeDamageSound);
        base.TakeDamage(damage);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        // target positon set to current position 
        targetMovingPosition = transform.position;

        // set animator + starting animarion to idling and facing south
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
        animator.SetFloat("movingDirection", 4f);

        // play spawn sound
        audioManager.Play(spawnSound);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        // entering turret range
        if (other.tag == ennemyTag) {
            if (other.gameObject.GetComponent<TowerRange>()) {
                Tower tower = other.gameObject.GetComponent<TowerRange>().tower;
                tower.targets.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // leaving turret range
        if (other.tag == ennemyTag && other.gameObject.GetComponent<TowerRange>()) {
            Tower tower = other.gameObject.GetComponent<TowerRange>().tower;
            tower.targets.Remove(gameObject);
            if (tower.currentTarget == gameObject) {
                tower.currentTarget = null;
            }
        }
    }

    private int DirectionToIndex(Vector2 _direction) {
        Vector2 norDir = _direction.normalized;

        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;

        if (angle < 0) {
            angle += 360; //normalize angles --> -45 becomes 325 to create proper rotation
        }

        float stepCount = angle / step;
        return (int)stepCount;
    }

    public void moveToPosition(Vector3 newPosition) {
        // unit is moving now, no longer attacking
        attacking = false;
        // define new position to move to in Update
        targetMovingPosition = newPosition;
        // take a sound at random from the array and plays it
        int nbSounds = acknowledgeSounds.Length;
        System.Random random = new System.Random();
        int soundIndex = random.Next(nbSounds);
        audioManager.Play(acknowledgeSounds[soundIndex]);
    }

    protected override void Die() {
        audioManager.Play(deathSound);
        // isDead boolean -> true
        // death animation
        // deactivate collider
        // destroy in XX seconds to leave a body on the ground for 10 seconds
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetMovingPosition, Time.deltaTime * speed);

        Vector2 direction = targetMovingPosition - (Vector2)transform.position;
        if (direction.magnitude >= 0.01f) {
            // make sure player is not attacking while moving
            animator.SetBool("isAttacking", false);
            //unit is moving, apply moving animation
            int movingAnimationIndex = DirectionToIndex(direction);
            animator.SetBool("isMoving", true);
            animator.SetFloat("movingDirection", (float)movingAnimationIndex);
        } else {
            //unit is not moving, apply idle animation
            animator.SetBool("isMoving", false);
            if (attackingInRange) {
                // handle attack
                animator.SetBool("isAttacking", true);
            }
        }

        if (attacking) {
            // if target is dead, we are no longer attacking it!
            if (ennemyTarget == null) {
                attacking = false;
                attackingInRange = false;
                animator.SetBool("isAttacking", false);
            } else {
                // define wether we are in range to melee attack ennemy unit
                // this is done thanks to a differential between unit position and ennemy position
                // ennemy entity holds an offset value to define how far from the center of it, this unit can melee attack it
                attackingInRange = (transform.position - ennemyTarget.transform.position).magnitude - ennemyTarget.offsetMeleeRangeForAttackers <= 0.1f;
                if (attackingInRange) {
                    //stop moving, and attack
                    targetMovingPosition = transform.position;
                    //checking wether we are still in range of 
                    attackTimer += Time.deltaTime;
                    if (attackTimer >= 60 / attackSpeed) {
                        ennemyTarget.TakeDamage(damage);
                        attackTimer = 0;
                    }
                } else {
                    targetMovingPosition = ennemyTarget.gameObject.transform.position;
                }
            }
        }
    }
}