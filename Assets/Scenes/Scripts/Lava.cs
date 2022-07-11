using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float scaleSpeed;
    public Transform lavaParticles;
    public bool stopScale;
    public bool stopMove;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "MenuLava"){
            stopMove = true;
        }
        StartCoroutine(increaseDifficulty());
    }

    // Update is called once per frame
    void Update()
    {  
        if(player || stopMove){
            if(gameObject.tag == "MenuLava" && transform.localScale.y > 6){
                stopScale = true;
            }


            if(transform.localScale.y > 12 && !stopMove){
                transform.position = new Vector2(transform.position.x,transform.position.y + scaleSpeed * Time.deltaTime);
                

            }else{
                if(!stopScale){
                transform.localScale = new Vector2(transform.localScale.x,transform.localScale.y + scaleSpeed * Time.deltaTime);
                
                }
                
            }
            if(!stopScale){
                lavaParticles.transform.position = new Vector2(lavaParticles.transform.position.x,lavaParticles.transform.position.y + scaleSpeed * Time.deltaTime);
            }
        }
        
    }

    IEnumerator increaseDifficulty(){

        while(true){
            yield return new WaitForSeconds(10);
            if(transform.localScale.y > 12){
                scaleSpeed += 0.08f;
            }
            

        }
        
    }
}
