using UnityEngine;
using UnityEngine.UI;

namespace Foggynails
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image _icon;
        private InventoryItem _item;

        public void AddItem(InventoryItem item)
        {
            _item = item;
            _icon.sprite = item.icon;
            _icon.enabled = true;
        }

        public void Clear()
        {
            _item = null;
            _icon.sprite = null;
            _icon.enabled = false;
        }
    }

}
