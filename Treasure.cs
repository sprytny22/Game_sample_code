using Foggynails;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Treasure : MonoBehaviour, IClickable
{
    private bool _isClicked;
    private bool _isLocked;
    private Character[] _characters;

    [SerializeField]
    private List<InventoryItem> _itemPool;

    public bool isClicked { get { return _isClicked; } set { _isClicked = value; } }
    public bool isLocked { get { return _isLocked; } set { _isLocked = value; } }

    private void Start()
    {
        isClicked = false;
        isLocked = false;
        _characters = FindObjectsOfType<Character>();

    }
    private void Update()
    {
        foreach(Character character in _characters)
        {
            var distance = Vector3.Distance(character.transform.position, transform.position);
            Debug.Log("distance:" + distance.ToString());

            if (distance < 1.5)
            {
                if (!isLocked)
                {
                    Observer.Publish(new TreasureGiveItemEvent(character, randomItem()));
                    isLocked = true;
                }
            }
        }
    }

    private InventoryItem randomItem()
    {
        var items_count = _itemPool.Count;
        if (items_count == 0)
        {
            Debug.LogError("No items in item pool.");
            return null;
        }
        int random = Random.Range(0, items_count);

        return _itemPool[random];
    }

    public void onClick()
    {
        Observer.Publish(new ExecuteEffectEvent(transform.position, "Treasure_On_Click_Effect"));
    }

    public void onHover()
    {

    }

    public bool Contains(Vector3 position)
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (isClicked)
        {
            if (collider.bounds.Contains(position))
            {
                isClicked = false;
                return true;
            }
        }
        return false;
    }
}
