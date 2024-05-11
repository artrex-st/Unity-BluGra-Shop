namespace Source.InventoryServices
{
    public enum ItemTypes
    {
        Head,
        Chest,
        Gloves,
        Boots,
        Coin
    }

    public class Item
    {
        public ItemTypes ItemType;
        public int Amount;
    }
}
