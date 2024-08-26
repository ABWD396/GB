class Guard : UnitMain
{
    static public new float timerStatic = 3;
    static public new int foodStatic = 3;
    static public new int priceStatic = 5;
    public Guard()
    {
        resourceType = ResourceType.Guards;
    }

    public override void UpdateValues()
    {
        timer = timerStatic;
        food = foodStatic;
        price = priceStatic;
    }
}