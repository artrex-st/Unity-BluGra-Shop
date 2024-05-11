using System.Collections.Generic;

namespace Source.InventoryServices
{
    public interface IInventoryService
    {
        public List<Item> ItemsList { get; }
        public void AddItem(Item newItem);
    }
}
