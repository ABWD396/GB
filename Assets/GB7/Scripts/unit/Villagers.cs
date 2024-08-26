class Villagers : UnitMain
{
    static public new float timerStatic = 2;
    static public new int foodStatic = 1;
    static public new int priceStatic = 2;
    static public int wheatProduceCount = 2;
    public Villagers()
    {
        resourceType = ResourceType.Villagers;
    }

    public override void UpdateValues()
    {
        timer = timerStatic;
        food = foodStatic;
        price = priceStatic;
    }
}