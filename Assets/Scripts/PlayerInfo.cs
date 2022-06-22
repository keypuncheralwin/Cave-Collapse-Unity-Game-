using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float moveSpeed;
    public float jumpVelocity;
    public float jumpBufferTime;
    public float jumpLimit;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundSize;
    public LayerMask whatIsGround;
    public float coyoteTime;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float stompGravity;
    public float wallJumpTime;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    public bool isWallSliding = false;
    public LayerMask whatIsBox;
    public bool isOnBox;
    public bool playerDied = false;
    
}
