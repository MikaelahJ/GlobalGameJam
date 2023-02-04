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
    public ResourceManager resourceManager;

    //LIST OF ALL ROOTS

    public float waterResource;
    public float carbonResource;


    // Start is called before the first frame update
    void Start()
    {
        currentState = gameState.playing;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {                                                           // you are playing
            case gameState.playing:
                waterResource = resourceManager.getWater();
                carbonResource = resourceManager.getCarbon();
                resourceManager.drainAllResourcePoints();
                resourceManager.rekteningTime();
                displayResources();
                break;
        }
    }
    public void displayResources()
    {
        tmpWaterCounter.text = "" + (int)waterResource;
        tmpCarbonCounter.text = "" + (int)carbonResource;
    }
    

}
