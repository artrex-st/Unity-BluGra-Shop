using System.Collections.Generic;
using UnityEngine;

namespace Source.InventoryServices
{
    public class InventoryService : BaseService, IInventoryService
    {
        public List<Item> ItemsList { get; set; } = new List<Item>();

        public override void Setup()
        {
            ServiceLocator.Instance.RegisterService<IInventoryService>(this);
            Debug.Log("Inventory initialized.");
            AddItem(new Item{Amount = 1, ItemType = ItemTypes.Coin});
        }

        public void AddItem(Item newItem)
        {
            ItemsList.Add(newItem);
        }
    }
}
