using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public Transform player;
    public Transform background;
    public Transform lava;
    public float scaleSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(moveBackground());
       StartCoroutine(increaseDifficulty());
    }

    // Update is called once per frame
    void Update()
    {   
        if(player){
        if(player.transform.position.y > 12 && !player.GetComponent<PlayerInfo>().playerDied)
            {
            transform.position = new Vector2(transform.position.x,transform.position.y + scaleSpeed * Time.deltaTime); 
            }
        }        
    }


    IEnumerator moveBackground(){

        while(true){
            yield return new WaitForSeconds(15);
            background.position = new Vector2(background.position.x, background.position.y + 6 + scaleSpeed);
            

        }
        
    }

        IEnumerator increaseDifficulty(){

        while(true){
            yield return new WaitForSeconds(10);
            if(lava.transform.localScale.y > 12){
                scaleSpeed += 0.1f;
            }
            

        }
        
    }
}
