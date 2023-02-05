using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrower : MonoBehaviour
{
    [SerializeField] private Slider plantSliderPrefab;
    private Slider plantSlider;
    private ResourceManager resourceManager;

    public bool isRunning;
    public bool canGetWater;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        plantSlider = Instantiate(plantSliderPrefab, transform.GetChild(0));
    }


    public void StartGrow(int availableWater)
    {
        if (isRunning)
            return;

        StartCoroutine(GrowPlant(availableWater));

    }

    public IEnumerator GrowPlant(int availableWater)
    {
        isRunning = true;

        for (int i = 0; i < availableWater; i++)
        {
            yield return new WaitForSeconds(1);
            plantSlider.value += 1;
            resourceManager.RemoveWater();
        }
        isRunning = false;
    }
}
