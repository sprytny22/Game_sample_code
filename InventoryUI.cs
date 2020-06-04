using System.Linq.Expressions;
using UnityEngine;

namespace Foggynails
{
    public class InventoryUI : MonoBehaviour
    {

        //Try make it with scriptable object
        public GameObject mainUI;
        public Transform slotsParent;
        private InventorySlotUI[] _slots;
        private bool _switcher = false;

        private void Start()
        {
            _slots = slotsParent.GetComponentsInChildren<InventorySlotUI>();
            Observer.RegisterListener<UpdateInventoryUiEvent>(UpdateUI);
            Observer.RegisterListener<OnEnableInventoryUIEvent>(OnEnableUI);

        }

        private void UpdateUI(object publishedObject)
        {
            UpdateInventoryUiEvent e = publishedObject as UpdateInventoryUiEvent;

            var inv = e.Inv;

            for (int i = 0; i < inv._items.Count; i++)
            {
                if (inv._items[i] != null)
                {
                    _slots[i].AddItem(inv._items[i]);
                }
                else
                {
                    _slots[i].Clear();
                }
            }
        }

        private void OnEnableUI(object publishedObject)
        {
            if (_switcher)
            {
                _switcher = false;
                mainUI.SetActive(true);
            }else
            {
                _switcher = true;
                mainUI.SetActive(false);
            }
        }
    }
}
