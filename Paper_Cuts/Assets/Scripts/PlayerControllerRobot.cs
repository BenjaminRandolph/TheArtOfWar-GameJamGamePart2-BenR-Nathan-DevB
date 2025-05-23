using UnityEngine;
using System.Collections;

public class PlayerControllerRobot : MonoBehaviour
{
    // ** Movement **
    // the maximum speed that the player can accelerate to
    [SerializeField]
    private float maxSpeed = 5.0f;
    // acceleration of the player (increment changes the player speed by its amount each frame)
    [SerializeField]
    private float increment = 0.1f;
    // how far the player can jump
    [SerializeField]
    private float jumpForce = 10.0f;
    // bool that stores whether the player can jump or not (whether they are standing on something)
    [SerializeField]
    private bool grounded = false;
    // whether the player can actually move or attack (so that we can disable their actions while they are already attacking)
    private bool canMoveOrAttack = true;
    // stores the direction that the player last tried to move
    private string lastDirectionMoved = "right";

    // ** Attacks **
    // how much damage they do to other players
    [SerializeField]
    private int baseAttackDamage = 10;
    // how far the attack checks for enemies to damage
    [SerializeField]
    private float rangeOfAttack = 4.0f;
    // how long it takes before the attack can do damage once it has been told to start
    [SerializeField]
    private float startAttackLag = 0.2f;
    // how long it takes before the next attack can be done where the previous attack doesn't do damage
    [SerializeField]
    private float endAttackLag = 0.3f;
    // how long you are frozen while you attack
    [SerializeField]
    private float freezeTime = 1.0f;
    // whether damage can be done by the attack
    [SerializeField]
    private bool canDoDamage = false;
    // stores the direction of the last submitted attack
    [SerializeField]
    private string previousAttackDirection;
    // stores whether we have already hit the enemy for this attack
    [SerializeField]
    private bool repeatHit = false;
    // base knockback for attacks (this is multiplied by a fourth of the attack damage done)
    [SerializeField]
    private float baseKnockback = 2.0f;

    // ** Health **
    // how much damage they can take in total
    [SerializeField]
    public int health = 100;

    // ** Keys to Watch For Input **
    [SerializeField]
    private KeyCode up = KeyCode.W;
    [SerializeField]
    private KeyCode down = KeyCode.S;
    [SerializeField]
    private KeyCode left = KeyCode.A;
    [SerializeField]
    private KeyCode right = KeyCode.D;
    [SerializeField]
    private KeyCode jump = KeyCode.Space;
    [SerializeField]
    private KeyCode attackRegular = KeyCode.G;

    // ** Objects To Include **
    // the way we change the animation controller's parameters so it does different animations
    private Animator animator;
    // the rigidbody of our player
    private Rigidbody2D rb;
    // This is used to find the size of the player in units (which for some reason it stores but the gameobject doesn't)
    private SpriteRenderer thingThatLetsMeFindTheSizeOfTheObject;
    // object to store the result of the raycast to check for ground
    private RaycastHit2D hit;
    // the object to store the size of the player from the SpriteRenderer
    private Vector2 size;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thingThatLetsMeFindTheSizeOfTheObject = GetComponent<SpriteRenderer>();
        animator = GameObject.Find("RobotAnimated").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // shoot out a ray to find whether the player is actually touching the ground 
        hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 1.2f + 0.1f);

        // ground check
        if (hit){
            grounded = true;
            //Debug.Log(hit.collider);
            animator.SetBool("grounded", grounded);
        }else{
            grounded = false;
            //Debug.Log("not ground");
            animator.SetBool("grounded", grounded);
        }

        // check if the player wants to jump
        if( Input.GetKeyDown(jump) && grounded && canMoveOrAttack ){
            animator.SetBool("jumping", true);
            Jump();
        }else if( Input.GetKeyUp(jump) || !canMoveOrAttack ){
            animator.SetBool("jumping", false);
        }

        if( (Input.GetKey(left) || Input.GetKey(right)) && canMoveOrAttack ){
            // check if the player wants to move left
            if( Input.GetKey(left) && canMoveOrAttack ){
                Vector3 scale = transform.localScale;
                if( Mathf.Sign(scale.x) == -1 ){
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                lastDirectionMoved = "left";
                animator.SetBool("running", true);
                Move(-increment);
            }

            // check if the player wants to move right
            if( Input.GetKey(right) && canMoveOrAttack ){
                Vector3 scale = transform.localScale;
                if( Mathf.Sign(scale.x) == 1 ){
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                lastDirectionMoved = "right";
                animator.SetBool("running", true);
                Move(increment);
            }
        }else{
            animator.SetBool("running", false);
        }
        

        // check if the player wants to attack
        if( Input.GetKeyDown(attackRegular) && canMoveOrAttack ){
            if(Input.GetKey(up)){
                previousAttackDirection = "up";
                animator.SetInteger("attackDirection", 1);
            }else if(Input.GetKey(down)){
                previousAttackDirection = "down";
                animator.SetInteger("attackDirection", 2);
            }else if( Input.GetKey(left) || lastDirectionMoved == "left" ){
                previousAttackDirection = "left";
                animator.SetInteger("attackDirection", 0);
            }else{
                previousAttackDirection = "right";
                animator.SetInteger("attackDirection", 0);
            }

            IEnumerator coroutineToRun = AttackFreeze(freezeTime);
            StartCoroutine(coroutineToRun);
            StartCoroutine("AttackDamageChange");
        }

        // the attack should continue to check for something to damage when in the damaging phase of the attack
        // later it does also check to make sure that the thing it hit won't take damage any more than once in a single attack
        if(canDoDamage){
            Attack(previousAttackDirection);
        }
    }

    // make the player jump
    private void Jump(){
        rb.linearVelocityY += jumpForce;
    }

    // make the player move in a direction
    private void Move( float incrementDirectional ){
        if(incrementDirectional < 0){
            if( rb.linearVelocityX > -maxSpeed ){
                rb.linearVelocityX += incrementDirectional;
            }
        }else if(incrementDirectional > 0){
            if( rb.linearVelocityX < maxSpeed ){
                rb.linearVelocityX += incrementDirectional;
            }
        }
    }

    // make the player attack
    private void Attack(string direction){
        //Debug.Log("attacking in this direction: " + direction);

        Vector2 modifiedPosition = transform.position;
        Vector2 directionVector;
        if(direction == "up"){
            directionVector = new Vector2(0, 1);
        }else if(direction == "down"){
            if(grounded){
                if(lastDirectionMoved == "left"){
                    directionVector = new Vector2(-1, 0);
                }else{
                    directionVector = new Vector2(1, 0);
                }
                
                modifiedPosition.y = modifiedPosition.y - size.y / 4;
            }else{
                directionVector = new Vector2(0, -1);
            }
        }else if(direction == "left"){
            directionVector = new Vector2(-1, 0);
        }else{
            directionVector = new Vector2(1, 0);
        }


        RaycastHit2D hit2;
        hit2 = Physics2D.Raycast(modifiedPosition, directionVector, rangeOfAttack);

        if(hit2 && !repeatHit){
            Debug.Log("Hit something!" + hit2.collider.gameObject.name);
            if(hit2.collider.gameObject.GetComponent<PlayerController>() != null){
                hit2.collider.gameObject.GetComponent<PlayerController>().TakeDamage(baseAttackDamage);
            }else if(hit2.collider.gameObject.GetComponent<PlayerController>() != null){
                hit2.collider.gameObject.GetComponent<PlayerControllerWolf>().TakeDamage(baseAttackDamage);
            }else if(hit2.collider.gameObject.GetComponent<PlayerControllerRobot>() != null){
                hit2.collider.gameObject.GetComponent<PlayerControllerRobot>().TakeDamage(baseAttackDamage);
            }else if(hit2.collider.gameObject.GetComponent<PlayerControllerStick>()){
                hit2.collider.gameObject.GetComponent<PlayerControllerStick>().TakeDamage(baseAttackDamage);
            }

            if(hit2.collider.gameObject.GetComponent<Rigidbody2D>() != null){
                Rigidbody2D hitRB = hit2.collider.gameObject.GetComponent<Rigidbody2D>();

                if( hit2.transform.position.x < transform.position.x ){
                    hitRB.linearVelocityX -= baseKnockback * baseAttackDamage / 4;
                    hitRB.linearVelocityY += baseKnockback * baseAttackDamage / 4;
                }else{
                    hitRB.linearVelocityX += baseKnockback * baseAttackDamage / 4;
                    hitRB.linearVelocityY += baseKnockback * baseAttackDamage / 4;
                }
            }
            Debug.Log("did damage to it!");
            repeatHit = true;
        }
    }

    // coroutine to freeze the players actions for a time
    IEnumerator AttackFreeze(float timeToWait){
        animator.SetBool("attacking", true);
        canMoveOrAttack = false;
        yield return new WaitForSeconds(timeToWait);
        canMoveOrAttack = true;
        animator.SetBool("attacking", false);
    }

    // coroutine to let the attack do damage for a certain period of time
    IEnumerator AttackDamageChange(){
        canDoDamage = false;
        yield return new WaitForSeconds(startAttackLag);
        canDoDamage = true;
        yield return new WaitForSeconds(freezeTime - startAttackLag - endAttackLag);
        canDoDamage = false;
        repeatHit = false;
    }

    // make the player recieve damage
    public void TakeDamage( int damageToTake){
        animator.SetBool("gotHit", true);
        health -= damageToTake;
        if(health < 0){
            Die();
        }
        animator.SetBool("gotHit", false);
    }

    private void Die(){
        canMoveOrAttack = false;
        animator.SetBool("dead", true);
    }
}
