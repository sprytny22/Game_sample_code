using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Foggynails
{
    public class StatisticsUI : MonoBehaviour
    {
        private MainHero _mainHero = null;
        private List<Hero> _heroes = null;

        public Image[] UIHeroAvatars;

        private void Awake()
        {
            Observer.RegisterListener<InitStatisticsUiEvent>(InitStatisticsUI);

            if (_mainHero == null || _heroes == null)
                Debug.LogError("Statistics UI null error.");
        }

        public void InitStatisticsUI(object publishedObject)
        {
            InitStatisticsUiEvent e = publishedObject as InitStatisticsUiEvent;

            _mainHero = e.HeadHero;
            _heroes = e.Heroes;

            setHeroesToPanel();
        }

        private void setHeroesToPanel()
        {
            
        }
    }
}

