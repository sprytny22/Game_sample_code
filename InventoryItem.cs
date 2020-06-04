using UnityEngine;
using UnityEngine.UI;

namespace Foggynails
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Items", order = 1)]
    public class InventoryItem : ScriptableObject
    {
        public string name;
        public string description;
        public Sprite icon;
    }
}
