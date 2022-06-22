using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float scaleSpeed = 0.5f;
    public Transform lavaParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  

        if(transform.localScale.y > 12){
            transform.position = new Vector2(transform.position.x,transform.position.y + scaleSpeed * Time.deltaTime);
            Debug.Log("MOVING");

        }else{
            transform.localScale = new Vector2(transform.localScale.x,transform.localScale.y + scaleSpeed * Time.deltaTime);
            Debug.Log("SCALING");
        }
        
        lavaParticles.transform.position = new Vector2(lavaParticles.transform.position.x,lavaParticles.transform.position.y + scaleSpeed * Time.deltaTime);
    }
}
