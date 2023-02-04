using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                                                                                        // TO DO : 
                                                                                        // ENABLE PURCHASES!
                                                                                        // FUNCTION FOR DRAINING WATER
public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance;  } }
    public float waterSupply;
    public float carbonSupply;
    public GameObject carbonPile;
    public List<Resource> allResources;
    public List<GameObject> allGOResourcePoints = new List<GameObject>();
  //  public List<ResourcePoint> allResourcePoints = new List<ResourcePoint>();

    public int idCounter = 99;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public float getWater()
    {
        float totalWaterInput = 0.0f;
        foreach (Resource resurs in allResources)
        {
            if (resurs.getType() == "water")
            {
                totalWaterInput += resurs.getYield() * (Time.deltaTime * 1); ;
            }
        }
        waterSupply = totalWaterInput;
        return totalWaterInput;
    }

    public float getCarbon()
    {
        float totalCarbonInput = 0.0f;
        foreach (Resource resurs in allResources)
        {
            if (resurs.getType() == "carbon")
            {
                totalCarbonInput += resurs.getYield() * (Time.deltaTime * 1);
            }
        }
        carbonSupply = totalCarbonInput;
        return totalCarbonInput;
    }
    //______________________________________________ L�GG TILL RESURSER ATT SAMLA UPP I SLUTET
    public void addResources(Resource resurs)
    {
        bool freeToAdd = true;
        foreach (Resource listResurs in allResources)
        {
            if (resurs.getId() == listResurs.getId()) freeToAdd = false;
        }
        if (freeToAdd) allResources.Add(resurs);
    }
    public void removeResource(Resource resurs)
    {
        foreach (Resource listResurs in allResources)
        {
            if (resurs.getId() == listResurs.getId())
            {
                allResources.Remove(listResurs);
            }
        }
    }
   
    public int getNewId()
    {
        idCounter--;
        return idCounter;
    }

    public void spawnInCarbon(Vector3 position)
    {
        GameObject _carbonPile = Instantiate(carbonPile);
        _carbonPile.transform.position = position;
        var resourcePoint = _carbonPile.GetComponent<ResourcePoint>();
        resourcePoint.setId(getNewId());
        allGOResourcePoints.Add(_carbonPile);
    }


    public void rekteningTime()
    {
        foreach (GameObject go in allGOResourcePoints)
        {
            var resourcePoint = go.GetComponent<ResourcePoint>();
            ResourcePoint rs = resourcePoint;
            if(!rs.isAlive()) Destroy(go); 
        }
    }


    /*
    public void addResourcePointToList(ResourcePoint resourcePoint)
    {
        allResourcePoints.Add(resourcePoint);
    }
    public void addStartinGOresourcePoints()                            Plocka lista med ResourcePoints
    {
        List<ResourcePoint> test = new List<ResourcePoint>();

        foreach (GameObject o in allGOResourcePoints)
        {
            var testt = o.GetComponent<ResourcePoint>();
            if (testt != null)
                test.Add(testt);
        }
        allResourcePoints = test;
    }
    */


}