using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
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
    [SerializeField]
    private bool grounded = false;
    private bool canMoveOrAttack = true;

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

    // ** Health **
    // how much damage they can take in total
    [SerializeField]
    private int health = 100;

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
    // the rigidbody of our player
    private Rigidbody2D rb;
    // I'm not going to explain these lol
    private SpriteRenderer thingThatLetsMeFindTheSizeOfTheObject;
    private RaycastHit2D hit;
    private Vector2 size;
    private Vector2 currentPositionModified;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thingThatLetsMeFindTheSizeOfTheObject = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        size = thingThatLetsMeFindTheSizeOfTheObject.bounds.size;
        hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), size.y/2 + 0.1f);

        // ground check
        if (hit){
            grounded = true;
        }else{
            grounded = false;
        }

        // need ground raycast
        if( Input.GetKeyDown(jump) && grounded && canMoveOrAttack ){
            Jump();
        }

        if( Input.GetKey(left) && canMoveOrAttack ){
            Move(-increment);
        }

        if( Input.GetKey(right) && canMoveOrAttack ){
            Move(increment);
        }

        if( Input.GetKeyDown(attackRegular) && canMoveOrAttack ){
            IEnumerator coroutineToRun = AttackFreeze(freezeTime);
            StartCoroutine(coroutineToRun);
            StartCoroutine("AttackDamageChange");

            if(Input.GetKey(up)){
                previousAttackDirection = "up";
                Attack("up");
            }else if(Input.GetKey(down)){
                previousAttackDirection = "down";
                Attack("down");
            }else if( Input.GetKey(left) || ( !Input.GetKey(right) && (rb.linearVelocityX < 0.0f) ) ){
                previousAttackDirection = "left";
                Attack("left");
            }else{
                previousAttackDirection = "right";
                Attack("right");
            }
        }

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
        Debug.Log("attacking in this direction: " + direction);

        Vector2 directionVector;
        if(direction == "up"){
            directionVector = new Vector2(0, 1);
        }else if(direction == "down"){
            directionVector = new Vector2(0, -1);
        }else if(direction == "left"){
            directionVector = new Vector2(-1, 0);
        }else{
            directionVector = new Vector2(1, 0);
        }

        /*
        int layerForRaycast = 6;
        if(this.gameObject.layer == 7){
            layerForRaycast = 7;
        }
        */

        RaycastHit2D hit2;
        hit2 = Physics2D.Raycast(transform.position, directionVector, rangeOfAttack);

        if(hit2 && !repeatHit){
            Debug.Log("Hit something!" + hit2.collider.gameObject.name);
            hit2.collider.gameObject.GetComponent<PlayerController>().TakeDamage(baseAttackDamage);
            hit2.collider.gameObject.GetComponent<Rigidbody2D>().linearVelocityX += 2.0f;
            hit2.collider.gameObject.GetComponent<Rigidbody2D>().linearVelocityY += 2.0f;
            Debug.Log("did damage to it!");
            repeatHit = true;
        }
    }

    IEnumerator AttackFreeze(float timeToWait){
        canMoveOrAttack = false;
        yield return new WaitForSeconds(timeToWait);
        canMoveOrAttack = true;
    }

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
        health -= damageToTake;
        if(health < 0){
            Die();
        }
    }

    private void Die(){

    }
}
