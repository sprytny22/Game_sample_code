using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Foggynails
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _cube_test_character; //TODO: its field for test only
        [SerializeField] private Transform _startCharacterPosition;
        [SerializeField] private Camera _camera;

        private Squad _player;

        private void Awake()
        {        
            //TODO: TEST AWAKE
            var go = Instantiate(_cube_test_character,_startCharacterPosition.position, Quaternion.identity);
            var heroes_list = new List<Hero>(0);
            _player = new Squad(go, heroes_list);
        }
        void Update()
        {
            inputActions();
        }

        private void inputActions()
        {
            if (Input.GetButtonDown("Fire1") && _camera != null) 
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                if (Physics.Raycast(ray, out var hit))
                {
                    var hittedObject = hit.collider.gameObject;
                    Observer.Publish(new CharacterClickEvent(hittedObject, hit.point));
                    var clickableObject = hittedObject.GetComponent<IClickable>();
                    clickableObject?.onClick();
                    
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Observer.Publish(new CharacterClickEvent(null , Vector3.zero));
                Observer.Publish(new CloseAlertItemUiEvent());
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                Observer.Publish(new OnEnableInventoryUIEvent());
            }
        }
    }
}

