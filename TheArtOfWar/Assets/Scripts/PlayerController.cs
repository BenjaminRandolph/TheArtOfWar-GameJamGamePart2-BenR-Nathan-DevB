using UnityEngine;

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
    private bool grounded = false;

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
        currentPositionModified = transform.position;
        currentPositionModified.y -= size.y/2 + 0.1f;
        hit = Physics2D.Raycast(currentPositionModified, new Vector2(0, -1), 0.1f);

        // ground check
        if (hit){
            grounded = true;
        }else{
            grounded = false;
        }

        // need ground raycast
        if( Input.GetKeyDown(jump) && grounded ){
            Jump();
        }

        if( Input.GetKey(left) ){
            Move(-increment);
        }

        if( Input.GetKey(right) ){
            Move(increment);
        }
    }

    // make the player jump
    void Jump(){
        rb.linearVelocityY += jumpForce;
    }

    // make the player move in a direction
    void Move( float incrementDirectional ){
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
    void Attack(){

    }

    // make the player recieve damage
    public void TakeDamage(){

    }
}
