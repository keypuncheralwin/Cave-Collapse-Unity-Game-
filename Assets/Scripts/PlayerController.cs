using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    private PlayerInfo player;
    private Rigidbody2D rb;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private float jumpCounter = 0;
    private bool isFacingRight;
    RaycastHit2D wallCheckHit;
    private float hangTime;
    private float wallJumpCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerInfo>();
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
    Movement();
    }
    void Movement()
    {
        //ground check
        player.isGrounded = Physics2D.OverlapCircle(player.groundCheck.position, player.groundSize, player.whatIsGround);
        player.isOnBox = Physics2D.OverlapCircle(player.groundCheck.position, player.groundSize, player.whatIsBox);
        if(player.isOnBox){
            player.isGrounded = true;
        }

        //left and right movement
        horizontalInput = Input.GetAxisRaw ("Horizontal");
        rb.velocity = new Vector2 (horizontalInput * player.moveSpeed, rb.velocity.y);

        //change sprite direction and update isFacingRight status
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2 (Mathf.Sign(rb.velocity.x), 1f);
        }
        if(transform.localScale.x == 1) isFacingRight = true; else isFacingRight = false;       
        
        //coyote time
        if(player.isGrounded) coyoteCounter = player.coyoteTime; else coyoteCounter -=Time.deltaTime;
        if(coyoteCounter > 0) player.isGrounded = true; else player.isGrounded = false;

        //jump buffer
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpBufferCounter = player.jumpBufferTime;
            jumpCounter += 1;
        }
        else
        { jumpBufferCounter -= Time.deltaTime;}
        bool jumped = false;
        if(jumpBufferCounter >= 0 ) jumped = true;

        //reset jumpLimit (double jump)
        if(player.isGrounded){
            jumpCounter = 0;
        }
        
        //jump
        if(player.isGrounded && jumped || jumped && jumpCounter < player.jumpLimit || jumped && player.isWallSliding && wallJumpCounter < player.jumpLimit){
            rb.velocity = new Vector2(rb.velocity.x, player.jumpVelocity);
            jumpBufferCounter = 0;
        }
        
        //stomp
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            rb.velocity = new Vector2(rb.velocity.x, -(transform.localScale.y * player.stompGravity));
        }

        //variable jump
        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1) * Time.deltaTime;
        }else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow)){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Wall Jump
        if (isFacingRight){
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(player.wallDistance, 0), player.wallDistance, player.whatIsBox);
            // Debug.DrawRay(transform.position, new Vector2(player.wallDistance, 0), Color.red);
        }else{
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-player.wallDistance, 0), player.wallDistance, player.whatIsBox);
            Debug.DrawRay(transform.position, new Vector2(-player.wallDistance, 0), Color.blue);
        }
        if (wallCheckHit && !player.isGrounded && horizontalInput !=0){
            player.isWallSliding = true;
            hangTime = Time.time + player.wallJumpTime;            
        }else if (hangTime < Time.time && !wallCheckHit){
            player.isWallSliding = false;
        }
        if (player.isWallSliding ){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -player.wallSlideSpeed, 10f));
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && player.isWallSliding){
            wallJumpCounter+=1;
        }
        if(!player.isWallSliding){
            wallJumpCounter = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered : " + other.gameObject.name);
        if(other.gameObject.tag == "Bar"){
            Debug.LogError("PLAYER DIED");
        }
        
    }
    
}
