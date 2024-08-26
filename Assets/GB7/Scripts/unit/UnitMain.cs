using System;
using UnityEngine;

public class UnitMain : MonoBehaviour
{

    static public float timerStatic = 0;
    static public int foodStatic = 0;
    static public int priceStatic = 0;
    public float timer = 0;
    public int food = 0;
    public int price = 0;

    public bool useDefault = false;
    public ResourceType priceType = ResourceType.Wheat;

    public ResourceType resourceType = ResourceType.None;

    public string GetName()
    {
        return Enum.GetName(typeof(ResourceType), resourceType);
    }

    void Awake()
    {
        if(useDefault){
            UpdateValues();
        }
    }

    public virtual void UpdateValues()
    {
        timer = timerStatic;
        food = foodStatic;
        price = priceStatic;
    }

}