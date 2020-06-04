using UnityEngine;
using UnityEngine.UI;

namespace Foggynails
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Hero/new Hero", order = 1)]
    public class Hero : ScriptableObject
    {
        public readonly string HeroName;
        public Image HeroAvatar;

        public float Health;
        public float Mana;
    }

    public class MainHero : Hero
    {
        private int _level;

        public int Level { get { return _level ;} }

        public void Levelup()
        {
            _level++;
        }
    }
}


