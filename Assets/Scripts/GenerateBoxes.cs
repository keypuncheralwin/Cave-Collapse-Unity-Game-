using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoxes : MonoBehaviour
{
    public GameObject[] boxes;
    public Transform player;
    public float respawnTime = 2f;
    private Vector2 screenBounds;
    public float playerOffset = 12.5f;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(boxWave());
    }

    private void spawnBox(){
        if(GameObject.FindGameObjectsWithTag("Box").Length > 80) {
            return;
        }
        // if(GameObject.FindGameObjectsWithTag("Box").Length  % 20 == 0 && respawnTime !< 0.1){
        //     Debug.Log("increase difficulty");
        //     Debug.Log(player.transform.position.y);
        //     respawnTime = respawnTime - 0.2f;
        //     Debug.Log(respawnTime);

        // }
        GameObject selectedPiece = boxes[Random.Range(0,boxes.Length-1)];
        GameObject a = Instantiate(selectedPiece);
        
        float boxSideGen = Random.Range(-4.27f, 4.27f);
        a.transform.position = new Vector2(boxSideGen,player.transform.position.y + playerOffset);
        
    }
    

    IEnumerator boxWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnBox();
        }
    }


}
