using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlantGrower : MonoBehaviour
{
    public AudioClip endNarrator;

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
            if(plantSlider.value == 100)
            {
                SceneManager.LoadScene(2);
            }
            if(plantSlider.value >= 85)
            {
                AudioManager.Instance.PlayNarrator(endNarrator);
            }
        }
        isRunning = false;
    }
}
