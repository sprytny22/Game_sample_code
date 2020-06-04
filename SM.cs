using System;
using UnityEngine;
using UnityEngine.AI;

namespace Foggynails
{
    public class SMidle : IState
    {
        private Character _character;
        public SMidle(Character character)
        {
            _character = character;
        }
        public void Tick() { }
        public void onEnter() { }
        public void onExit() { }
    }

    public class SMfocus : IState
    {
        private Character _character;
        private ParticleManager _pm;
        public SMfocus(Character character)
        {
            _character = character;
        }
        public void Tick() { }
        public void onEnter() {
            _character.setStatus(1);
            Observer.Publish(new ExecuteEffectEvent(_character.transform.position, "Character_Focus"));
        }
        public void onExit() {
            //_pm?.Reset();
        }
    }

    public class SMdisplayPath : IState
    {
        public static Action resetEffect;

        private Character _character;
        private NavMeshAgent _nav;
        private LineRenderer _line;
        private ParticleManager _pm;
        public SMdisplayPath(Character character, NavMeshAgent nav, LineRenderer line)
        {
            _character = character;
            _nav = nav;
            _line = line;
        }
        public void Tick() {
            getPath();
            DrawPath(_nav.path); //TODO: change it cause draw path every frame
        }
        public void onEnter() {
        }
        public void onExit() {
            _line.positionCount = 0;
            Observer.Publish(new VoidResetEvent());
        }

        private void getPath()
        {
            var clickPosition = _character.ClickPos;
            _line.positionCount = 2;
            _line.useWorldSpace = true;
            _line.SetPosition(0, _character.transform.position);
            _nav.SetDestination(clickPosition);
            _nav.isStopped = true;
        }

        private void DrawPath(NavMeshPath path)
        {
            if (path.corners.Length < 2) return;

            _line.positionCount = path.corners.Length;

            for (var i = 1; i < path.corners.Length; i++)
            {
                _line.SetPosition(i, path.corners[i]);
            }
        }
    }

    public class SMmoveToPoint : IState
    {
        private Character _character;
        private NavMeshAgent _nav;
        private float _timeStuck;

        private Vector3 clickPosition;

        public float TimeStuck { get { return _timeStuck; } set { _timeStuck = value; } }
        public SMmoveToPoint(Character character, NavMeshAgent nav)
        {
            _character = character;
            _nav = nav;
        }
        public void Tick() {
            var distance = Vector3.Distance(_character.transform.position, clickPosition);
            if (distance < 1.5f)
                TimeStuck += Time.deltaTime;


        }
        public void onEnter() {
            TimeStuck = 0f;
            clickPosition = _character.ClickPos;
            _nav.isStopped = false;
            _nav.SetDestination(_character.ClickPos);
        }
        public void onExit() { }
    }
}
