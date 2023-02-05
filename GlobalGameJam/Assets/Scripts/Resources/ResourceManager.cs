using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// TO DO : 
// ENABLE PURCHASES!
// FUNCTION FOR DRAINING WATER
public class ResourceManager : MonoBehaviour
{
    public AudioClip slurp;
    public AudioClip gravel;

    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance; } }
    public float waterSupply;
    public float carbonSupply;
    public GameObject carbonPile;
    public List<Resource> allResources;
    public List<GameObject> allGOResourcePoints = new List<GameObject>();
    //  public List<ResourcePoint> allResourcePoints = new List<ResourcePoint>();

    public int idCounter = 99;

    //Resource costs
    public const int UPGRADE_ROOT = 10;
    public const int UPGRADE_VISION = 10;
    public const int UPGRADE_RESOURCE = 10;
    public const int UPGRADE_DEFENCE = 10;
    public const int NEW_NODE = 3;
    public const int NEW_SUBROOT = 1;
    public const int REPAIR_SUBROOT = 1;

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
                totalWaterInput += resurs.getYield() * (Time.deltaTime * 1);
            }
        }
        waterSupply += totalWaterInput;

        if (waterSupply == 0 && !GetComponent<PlantGrower>().isRunning)
        {
            //TODO plant dies stuff, Bool plantDying = true
        }
        else if (waterSupply != 0 && !GetComponent<PlantGrower>().isRunning)
        {
            GetComponent<PlantGrower>().StartGrow((int)waterSupply);
        }

        return waterSupply;
    }

    public float getCarbonSupply()
    {
        return carbonSupply;
    }
    public float getWaterSupply()
    {
        return waterSupply;
    }

    public void RemoveWater()
    {
        waterSupply -= 1f;
        if (waterSupply < 0) waterSupply = 0;
    }
    public void RemoveCarbon(float x)
    {
        carbonSupply -= x;
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
        carbonSupply += totalCarbonInput;

        return carbonSupply;
    }
    //______________________________________________ LÄGG TILL RESURSER ATT SAMLA UPP I SLUTET
    public void addResources(Resource resurs)
    {
        bool freeToAdd = true;
        foreach (Resource listResurs in allResources)
        {
            if (resurs.getId() == listResurs.getId()) freeToAdd = false;
        }
        if (freeToAdd)
        {
            if (resurs.getType() == "water") AudioManager.Instance.Play(slurp);
            else AudioManager.Instance.Play(gravel);
            allResources.Add(resurs);
        }
    }
    public void removeResource(Resource resurs)
    {
        Resource resToRekt = null;
        bool foundOne = false;
        foreach (Resource listResurs in allResources)
        {
            if (resurs.getId() == listResurs.getId())
            {
                foundOne = true;
                resToRekt = listResurs;
                break;
            }
        }
        if (foundOne)
        {
            allResources.Remove(resToRekt);
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

    public void drainAllResourcePoints()
    {
        List<ResourcePoint> resourcePoints = new List<ResourcePoint>();
        foreach (GameObject go in allGOResourcePoints)
        {
            var rP = go.GetComponent<ResourcePoint>();
            resourcePoints.Add(rP);
        }
        //totalCarbonInput += resurs.getYield() * (Time.deltaTime * 1);
        foreach (Resource res in allResources)
        {
            var test = resourcePoints.Where(rp => rp.getId() == res.getId()).ToList();
            foreach (var v in test)
            {
                v.drain(v.getYield() * (Time.deltaTime * 1));
            }
        }
    }
    public void rekteningTime()
    {
        bool foundOneToDestroy = false;
        GameObject resourceToRekt = null;
        ResourcePoint rs = null;
        foreach (GameObject go in allGOResourcePoints)
        {
            var resourcePoint = go.GetComponent<ResourcePoint>();
            rs = resourcePoint;

            if (!rs.isAlive())
            {
                resourceToRekt = go;
                foundOneToDestroy = true;
                break;
            }
        }
        if (foundOneToDestroy)
        {
            allGOResourcePoints.Remove(resourceToRekt);
            removeResource(rs.pumpOut());
            Destroy(resourceToRekt);
        }

    }
    public bool CanBuyUpgrade(int cost)
    {
        float carbonSupply = getCarbonSupply();

        if (cost > carbonSupply)
        {
            Debug.Log("Can't afford");
            return false;
        }
        RemoveCarbon(cost);
        return true;
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
