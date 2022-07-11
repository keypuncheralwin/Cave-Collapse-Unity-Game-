using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{   
    public float counter;
    public TextMeshProUGUI textBox;
    public GameObject gameOver;
    private PlayerInfo player;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = counter.ToString();
        player = gameOver.GetComponent<PlayerInfo>();

        

    }

    // Update is called once per frame
    void Update()
    {   
        if(!player.playerDied){
            counter += Time.deltaTime;
            textBox.text = Mathf.Round(counter).ToString();
        }
        
    }

}
