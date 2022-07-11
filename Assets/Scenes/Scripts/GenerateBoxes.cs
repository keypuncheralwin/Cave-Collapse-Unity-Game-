using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateBoxes : MonoBehaviour
{
    public GameObject[] boxes;
    public Transform player;
    public float respawnTime = 2f;
    private Vector2 screenBounds;
    public float playerOffset;
    private float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(boxWave());
        StartCoroutine(increaseDifficulty());

    }

    private void spawnBox(){
        if(player == null){return;}
        if(GameObject.FindGameObjectsWithTag("Box").Length > 80) {
            return;
        }

        GameObject selectedPiece = boxes[Random.Range(0,boxes.Length-1)];
        GameObject a = Instantiate(selectedPiece);
        
        float boxSideGen = Random.Range(-4.27f, 4.27f);
        a.transform.position = new Vector2(boxSideGen,player.transform.position.y + playerOffset);
        a.GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
        a.GetComponent<Rigidbody2D>().AddTorque(180, ForceMode2D.Force);
    }
    void Update(){
        counter += Time.deltaTime;
        
    }
    IEnumerator boxWave(){

        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnBox();
            

        }
        
    }

    IEnumerator increaseDifficulty(){

        while(true){
            yield return new WaitForSeconds(10);
            if(respawnTime !> 0.2f){
                respawnTime -= 0.05f;
            }
            

        }
        
    }

    public void restartGame(){
        SceneManager.LoadScene("SampleScene");
    }
    public void mainMenu(){
        SceneManager.LoadScene("Menu");
    }

}
