using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public Transform player;
    public Transform background;
    public float scaleSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("moveBackground", 10, 15);
    }

    // Update is called once per frame
    void Update()
    {   
        if(player.transform.position.y > 12 && !player.GetComponent<PlayerInfo>().playerDied)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y + scaleSpeed * Time.deltaTime); 
        }
    }
    
    private void moveBackground(){
        background.position = new Vector2(background.position.x, background.position.y + 5);
    }
}
