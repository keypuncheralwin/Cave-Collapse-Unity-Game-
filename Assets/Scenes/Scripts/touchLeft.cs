using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class touchLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
                rb.velocity = new Vector2(playerInfo.moveSpeed * Force, rb.velocity.y);
                // player.transform.Translate(Force * Time.deltaTime, 0,0);
                playerAnimator.SetBool("isRunning",true);
                player.transform.localScale = new Vector2(-1,1);
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
