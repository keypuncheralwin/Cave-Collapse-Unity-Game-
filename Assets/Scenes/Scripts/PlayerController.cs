using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    Animator playerAnimator;
    CapsuleCollider2D myCollider;
    public ParticleSystem dust;
    public AudioSource jumpSound;
    public AudioSource deathSound;
    public AudioSource fireSound;
    bool isDeathDone = false;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreOver;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI exit;
    bool isPaused = false;
    public bool touchJump;
    public GameObject movementButton;
    public GameObject jumpButton;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerInfo>();
        rb = GetComponent<Rigidbody2D> ();
        playerAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        gameOver.enabled = false;
        scoreOver.enabled = false;
        restart.enabled = false;
        exit.enabled = false;
        movementButton.SetActive(false);
        jumpButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {   
        if(isPaused){
            return;
        }
        
        if(player.playerDied || isPaused){
            playerDeath();
            return;
        }
        //ground check
        player.isGrounded = Physics2D.OverlapCircle(player.groundCheck.position, player.groundSize, player.whatIsGround);
        player.isOnBox = Physics2D.OverlapCircle(player.groundCheck.position, player.groundSize, player.whatIsBox);
        if(player.isOnBox){
            player.isGrounded = true;
            movementButton.SetActive(true);
            jumpButton.SetActive(true);
        }

        //head Hit death
        player.isHeadHit = Physics2D.OverlapCircle(player.headCheck.position, player.headSize, player.whatIsHeadHit);
        if(player.isHeadHit){
            player.playerDied = true;
        }

        //left and right movement
        horizontalInput = SimpleInput.GetAxisRaw ("Horizontal");
        rb.velocity = new Vector2 (horizontalInput * player.moveSpeed, rb.velocity.y);

        //change sprite direction and update isFacingRight status
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
            playerAnimator.SetBool("isRunning",true);
            transform.localScale = new Vector2 (Mathf.Sign(rb.velocity.x), 1f);
        }else if(!player.isGrounded && !player.isWallSliding){
            playerAnimator.SetBool("isRunning",true);
        }else{
            playerAnimator.SetBool("isRunning",false);
        }
        if(transform.localScale.x == 1) isFacingRight = true; else isFacingRight = false;       
        
        //coyote time
        if(player.isGrounded) coyoteCounter = player.coyoteTime; else coyoteCounter -=Time.deltaTime;
        if(coyoteCounter > 0) player.isGrounded = true; else player.isGrounded = false;

        //jump buffer
        if(Input.GetKeyDown(KeyCode.UpArrow) || touchJump)
        {
            jumpBufferCounter = player.jumpBufferTime;
            jumpCounter += 1;
            touchJump = false;
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
            playerAnimator.SetBool("isRunning",true);
            jumpSound.Play();
            createDust();
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
            // Debug.DrawRay(transform.position, new Vector2(-player.wallDistance, 0), Color.blue);
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
        if(other.gameObject.tag == "Lava"){
            player.playerDied = true;
            playerDeath();
        }
        
    }

    private void playerDeath(){
        if(!isDeathDone)
        {
            movementButton.SetActive(false);
            jumpButton.SetActive(false);
            rb.velocity = new Vector2(0,0);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(transform.up * 1000);
            rb.AddTorque(180, ForceMode2D.Force);
            myCollider.enabled = false;
            deathSound.Play();
            Invoke("destroy", 2f);
            isDeathDone = true;
        }

    }
    private void destroy(){
        pauseText.enabled = false;
        score.enabled = false;
        Debug.LogError("PLAYER DIED");
        Destroy(gameObject);
        gameOver.enabled = true;
        scoreOver.enabled = true;
        restart.enabled = true;
        exit.enabled = true;
        scoreOver.text = "You lasted " + score.text + " seconds";
        
    }

    void createDust(){
        dust.Play();
    }

    public void PauseResume(){
        if(pauseText.text == "Pause" && !player.playerDied){
            fireSound.Stop();
            Time.timeScale = 0;
            pauseText.text = "Resume";
            isPaused = true;
            return;
        }
        if(pauseText.text == "Resume"){
            fireSound.Play();
            Time.timeScale = 1;
            pauseText.text = "Pause";
            isPaused = false;
            return;
        }
    }

    public void TouchJump(){
        touchJump = true;
    }


}
