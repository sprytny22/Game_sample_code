using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foggynails
{
    public class Squad
    {
        private const int MAX_TEAM_SIZE = 4;
        private GameObject _character;
        private MainHero _mainHero;
        private List<Hero> _heroes;
        private Inventory _inventory;

        public Squad(GameObject character, List<Hero> heroes) //TODO: remove item
        {
            Observer.RegisterListener<TreasureGiveItemEvent>(GiveItem);

            if (_heroes.Count > MAX_TEAM_SIZE)
                Debug.LogError("MAX_TEAM_SIZE Critical error!");

            _character = character;
            _heroes = heroes;
            _inventory = new Inventory();

            Observer.Publish(new InitStatisticsUiEvent(_mainHero,_heroes));
            
        }

        public void GiveItem(object publishedObject)
        {
            TreasureGiveItemEvent e = publishedObject as TreasureGiveItemEvent;
            var this_character = _character.GetComponent<Character>();
            var comming_character = e.CharacterInventory;


            if (this_character == comming_character)
            {
                _inventory.Add(e.Item);
                Observer.Publish(new UpdateInventoryUiEvent(_inventory));
                Observer.Publish(new UpdateAlertItemUiEvent(e.Item));
            }
        }
    }
}

