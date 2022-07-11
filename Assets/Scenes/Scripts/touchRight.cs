using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class touchRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    public GameObject player;
    public float Force;
    Animator playerAnimator;
    private PlayerInfo playerInfo;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        playerInfo = player.GetComponent<PlayerInfo>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
   
        if(!playerInfo.playerDied){
            if(isPressed){
                Vector3 move = new Vector2(1* playerInfo.moveSpeed *Time.deltaTime, rb.velocity.y) ;
                rb.MovePosition(move);
                playerAnimator.SetBool("isRunning",true);
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData){
        isPressed = true;
        
    }

    public void OnPointerUp(PointerEventData eventData){
        isPressed = false;
    }
}
