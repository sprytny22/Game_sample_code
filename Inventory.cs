using System.Collections.Generic;
using UnityEngine;

namespace Foggynails
{
    public class Inventory
    {
        private const int INVENTORY_SIZE = 18;

        public List<InventoryItem> _items;

        public Inventory(List<InventoryItem> itemList)
        {
            _items = itemList;
        }
        public Inventory(InventoryItem item)
        {
            _items = new List<InventoryItem>();
            _items.Add(item);
        }
        public Inventory()
        {
            _items = new List<InventoryItem>(0);
        }

        public void Add(InventoryItem item)
        {
            if (_items.Count < INVENTORY_SIZE)
            {
                _items.Add(item);
            }
        }
    }
}

