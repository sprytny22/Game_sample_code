using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


namespace Foggynails
{
    
    public class Character : MonoBehaviour, IClickable
    {
        public enum clickStatus
        {
            IDLE,
            CHARACTER,
            FRIST_CLICK,
            SECOND_CLICK,
            ESC
        }

        private GameObject _greenFlagEffect = null;
        private GameObject _characterFocusEffect = null;

        private Vector3 _clickPos = Vector3.zero;
        private Vector3 _fristClickPosition = Vector3.zero;
        private StateMachine _sm;
        private NavMeshAgent _agent;
        private LineRenderer _line;
        //private Animator _animator;
        private clickStatus _status = clickStatus.IDLE;

        public Vector3 ClickPos { get { return _clickPos; } set { _clickPos = value; }}

        public bool isClicked { get; set; }

        private void Awake()
        {
            Observer.RegisterListener<CharacterClickEvent>(onCharacterClickHandler);

            _agent = GetComponent<NavMeshAgent>();
            _line = GetComponent<LineRenderer>();

            _sm = new StateMachine();
            var idle = new SMidle(this);
            var focus = new SMfocus(this);
            var displayPath = new SMdisplayPath(this, _agent, _line);
            var moveToPoint = new SMmoveToPoint(this, _agent);

            Machine(idle, focus, Focused());
            Machine(focus, displayPath, Path());
            Machine(displayPath, focus, onEsc());
            Machine(displayPath, moveToPoint, Moving());
            Machine(moveToPoint, focus, OnPositionChanged());
            Machine(focus, idle, onEsc());

            void Machine(IState from, IState to, Func<bool> condition)
            {
                _sm.AddTransition(from, to, condition);
            }

            _sm.SetState(idle);

            Func<bool> Focused() => () => _status == clickStatus.CHARACTER;
            Func<bool> Path() => () => _status == clickStatus.FRIST_CLICK;
            Func<bool> Moving() => () => _status == clickStatus.SECOND_CLICK;
            Func<bool> onEsc() => () => _status == clickStatus.ESC;
            Func<bool> OnPositionChanged() => () => moveToPoint.TimeStuck > 1f;

         }

        private void Update()
        {
            _sm.Tick();
            //Debug.Log("stats: " + _status.ToString());
        }

        private void onCharacterClickHandler(object publishedEvent)
        {
            CharacterClickEvent e = publishedEvent as CharacterClickEvent;
            var position = e.ClickedPosition;
            IClickable obj = e.ClickedObject?.GetComponent<IClickable>();
            Character character = e.ClickedObject?.GetComponent<Character>();
            Observer.Publish(new ResetSingleEffectEvent("Character_On_Ground_Click_Effect"));
            
            if (character == this)
            {
                _status = clickStatus.CHARACTER;
            }
            else if (obj != null)
            {
                if (_status == clickStatus.FRIST_CLICK
                    && obj.Contains(_fristClickPosition))
                    _status = clickStatus.SECOND_CLICK;
                else if ((_status == clickStatus.FRIST_CLICK
                    && !obj.Contains(_fristClickPosition))
                    || _status == clickStatus.CHARACTER)
                {
                    _fristClickPosition = e.ClickedObject.transform.position;
                    obj.isClicked = true;
                    _status = clickStatus.FRIST_CLICK;
                }

                ClickPos = e.ClickedObject.transform.position;
            }
            else if (e.ClickedObject?.tag == "ground") //todo: hardcoded
            {
                if (_status == clickStatus.FRIST_CLICK
                    && Vector3.Distance(position, _fristClickPosition) < 0.5f)
                    _status = clickStatus.SECOND_CLICK;
                else if ((_status == clickStatus.FRIST_CLICK
                    && Vector3.Distance(position, _fristClickPosition) > 0.5f)
                    || _status == clickStatus.CHARACTER)
                {
                    Observer.Publish(new ExecuteEffectEvent(position, "Character_On_Ground_Click_Effect"));
                    _fristClickPosition = position;
                    _status = clickStatus.FRIST_CLICK;
                }

                ClickPos = position;
            }
            else if (e.ClickedObject == null)
            {
                _status = clickStatus.ESC;
                Observer.Publish(new VoidResetEvent());
                isClicked = false;
            }
        }

        public void setStatus(int v)
        {
            _status = (clickStatus)v;
        }

        public void onClick()
        {
        }

        public void onHover()
        {
        }

        public bool Contains(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }

}
