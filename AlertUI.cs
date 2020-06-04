using Foggynails;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class AlertUI : MonoBehaviour
{
    public GameObject self;
    public TMP_Text ItemText;
    public InventorySlotUI Slot;

    private void Awake()
    {
        Observer.RegisterListener<UpdateAlertItemUiEvent>(UpdateUI);
        Observer.RegisterListener<CloseAlertItemUiEvent>(CloseUI);
        self.SetActive(false);
    }

    public void UpdateUI(object publishedObject)
    {
        UpdateAlertItemUiEvent e = publishedObject as UpdateAlertItemUiEvent;

        self.SetActive(true);
        ItemText.SetText(e.Item.name);
        Slot.AddItem(e.Item);
    }
    public void CloseUI(object publishedObject)
    {
        self.SetActive(false);
    }
}
