using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockManager : MonoBehaviour
{
    RaycastHit2D rightBoxCheckHit;
    RaycastHit2D leftBoxCheckHit;
    RaycastHit2D topBoxCheckHit;
    RaycastHit2D bottomBoxCheckHit;
    public LayerMask whatIsBox;
    public float boxDistance = 0.5f;
    private Rigidbody2D rb;
    private bool canSink = false;
    public float maxWeight = 80f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(canSink){
            increaseMass();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Lava"){
           rb.constraints = RigidbodyConstraints2D.FreezePositionX;
           canSink = true;
           Invoke("destroy", 5f);
           
        }
    }

    private void increaseMass(){
       
        
        if(rb.mass <= maxWeight){
            rb.mass = Mathf.Lerp(rb.mass, maxWeight, 0.01f);
            
        }else{
            canSink = false;
        }
    }

    private void destroy(){
        Destroy(gameObject);
        
    }

}
