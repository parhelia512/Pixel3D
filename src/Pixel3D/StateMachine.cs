﻿using System;
using Pixel3D.Attributes;
using Pixel3D.Engine;

namespace Pixel3D
{
    /// <summary>State machine for types that exist within the game state</summary>
    public class StateMachine : StateProvider
    {
        public StateMachine()
        {
            CurrentState = GetState<State>();
        }


        public new class MethodTable : StateProvider.MethodTable
        {
            [AlwaysNullChecked]
            public Action<StateMachine, IUpdateContext, State> BeginState;

            [AlwaysNullChecked]
            public Action<StateMachine, IUpdateContext, State> EndState;
        }

        public MethodTable StateMethods { get { return (MethodTable)CurrentState.methodTable; } }
        public State CurrentState { get; private set; }


        public override string ToString()
        {
            return string.Format("{0} ({1})", GetType().Name, CurrentState != null ? CurrentState.GetType().Name : "(null)");
        }


        public void SetState<TState>(IUpdateContext updateContext, bool allowStateRestart = false) where TState : State, new()
        {
            _DirectlySetState(GetState<TState>(), updateContext, allowStateRestart);
        }


        /// <summary>Set a state from a previously found state object. Not for general use.</summary>
        public void _DirectlySetState(State nextState, IUpdateContext updateContext, bool allowStateRestart)
        {
            if(!allowStateRestart && ReferenceEquals(CurrentState, nextState))
                return; // Don't re-enter the same state
            
            if(StateMethods.EndState != null)
                StateMethods.EndState(this, updateContext, nextState);
            
            State previousState = CurrentState;
            
            CurrentState = nextState;

            if(StateMethods.BeginState != null)
                StateMethods.BeginState(this, updateContext, previousState);
        }

    }
}
