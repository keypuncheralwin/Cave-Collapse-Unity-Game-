using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate
    void FixedUpdate()
    {
        if(!player.GetComponent<PlayerInfo>().playerDied){
            Vector3 targetPosition = new Vector3(transform.position.x,player.position.y+2, transform.position.z);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        
    }
}
