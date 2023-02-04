using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MainLoopScript : MonoBehaviour
{
    public enum gameState { menu, playing, paused};
    private gameState currentState;
    public GameObject camera;
    public TextMeshProUGUI tmpWaterCounter;
    public TextMeshProUGUI tmpCarbonCounter;

    //LIST OF ALL ROOTS

    public float waterResource;
    public float dirtResource;


    // Start is called before the first frame update
    void Start()
    {
        waterResource = 10;
        dirtResource = 10;
        currentState = gameState.playing;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {                                                           // you are playing
            case gameState.playing:
                resourceTick();
                
                
                break;
        }
    }
    void resourceTick()
    {
        waterResource += (Time.deltaTime * 1);
        Debug.Log("water: "+waterResource);
        tmpWaterCounter.text = ""+(int)waterResource;
    }
}
