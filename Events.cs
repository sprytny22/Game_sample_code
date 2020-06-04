using Foggynails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteEffectEvent//Vector3 newPosition, String effectName
{
    public ExecuteEffectEvent(Vector3 position, string name)
    {
        Position = position;
        EffectName = name;
    }
    public Vector3 Position { get; set; }
    public string EffectName { get; set; }
}

public class ResetSingleEffectEvent 
{
    public ResetSingleEffectEvent(string name)
    {
        EffectName = name;
    }
    public string EffectName { get; set; }
}

public class CharacterClickEvent {
    public CharacterClickEvent(GameObject obj, Vector3 pos)
    {
        ClickedObject = obj;
        ClickedPosition = pos;
    }
    public GameObject ClickedObject { get; set; }
    public Vector3 ClickedPosition { get; set; }
}

public class TreasureGiveItemEvent
{
    public TreasureGiveItemEvent(Character ch, InventoryItem item)
    {
        CharacterInventory = ch;
        Item = item;
    }
    public Character CharacterInventory { get; set; }
    public InventoryItem Item { get; set; }
}

public class UpdateInventoryUiEvent
{
    public UpdateInventoryUiEvent(Inventory inventory)
    {
        Inv = inventory;
    }
    public Inventory Inv { get; set; }
}

public class VoidResetEvent { }

public class OnEnableInventoryUIEvent {}

public class UpdateAlertItemUiEvent
{
    public UpdateAlertItemUiEvent(InventoryItem item)
    {
        Item = item;
    }
    public InventoryItem Item { get; set; }
}

public class CloseAlertItemUiEvent { }

public class InitStatisticsUiEvent
{
    public MainHero HeadHero { get; set; }
    public List<Hero> Heroes { get; set; }

    public InitStatisticsUiEvent(MainHero mh, List<Hero> list)
    {
        HeadHero = mh;
        Heroes = list;
    }
}