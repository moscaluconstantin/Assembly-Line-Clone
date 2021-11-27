namespace GlobalManagers
{
    public enum PointerStateType
    {
        None = 0,
        BuySelection = 1,
        SellSelection = 2
    }
    
    public static class PointerStateManager
    {
        private static PointerStateType pointerState;
    }
}