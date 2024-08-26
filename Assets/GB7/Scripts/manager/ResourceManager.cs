using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Singleton]
public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private Text[] counters;

    [SerializeField]
    private int[] resources;

    [SerializeField]
    private UnitMain[] units;
    private int resourcesCount;

    [SerializeField]
    GameManagerScript gameManagerScript;

    void Awake()
    {
        InitManager();
    }

    public void InitManager(bool force = false)
    {
        resourcesCount = Enum.GetValues(typeof(ResourceType)).Length - 1;
        
        if (force)
        {
            resources = new int[resourcesCount];
            resources[(byte)ResourceType.Villagers] = 5;
            resources[(byte)ResourceType.Guards] = 2;
            resources[(byte)ResourceType.Wheat] = 50;
            resources[(byte)ResourceType.Raiders] = 2;
        }
        UpdateResourceCount();
    }

    public void AddResource(ResourceType resourceType, int count)
    {
        resources[(byte)resourceType] += count;
        UpdateResourceCount();
    }
    public void SetResource(ResourceType resourceType, int count)
    {
        resources[(byte)resourceType] = count;
        UpdateResourceCount();
    }
    public void RemoveResource(ResourceType resourceType, int count)
    {
        resources[(byte)resourceType] -= count;
        UpdateResourceCount();
    }

    private void UpdateResourceCount()
    {
        for (int i = 0; i < resourcesCount; i++)
        {
            if (counters.Count() - 1 < i)
            {
                continue;
            }

            counters[i].text = resources[i].ToString();
        }
    }

    public int[] GetResources()
    {
        return resources;
    }
    public int GetResource(ResourceType resourceType)
    {
        return resources[(int)resourceType];
    }
    public UnitMain GetUnit(ResourceType resourceType)
    {
        return units[(int)resourceType];
    }

}