using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foggynails
{
    public class StateMachine
    {
        private IState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
                return;

            _currentState?.onExit();
            _currentState = state;
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);


            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            _currentState.onEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            transitions.Add(new Transition(to, condition));
        }

        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private Transition GetTransition()
        {
            foreach (var transition in _currentTransitions)
            {
                if (transition.Condition())
                    return transition;
            }
            return null;
        }
    }

}
