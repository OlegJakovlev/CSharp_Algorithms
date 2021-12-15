using AlgorithmsAssignment.FiniteStateMachine.States;
using System;
using System.Collections.Generic;

namespace AlgorithmsAssignment.FiniteStateMachine
{
    class FSM
    {
        public State nextState  = null;
        public State currentState = null;

        public enum StateIDList {
            Menu,
            ChooseArray,
            ChooseSort,
            ChooseSortMode,
            ChooseSearch,
            ChooseSearchMode,
            ChooseValue,
            Merge
        }

        public Dictionary<int, State> possibleStates = new Dictionary<int, State>();

        public FSM()
        {
            InitializeAllTheStates();
            ChangeState(GetState((int)StateIDList.Menu));
        }

        private void InitializeAllTheStates()
        {
            AddState((int) StateIDList.Menu, new Menu(this));
            AddState((int) StateIDList.ChooseArray, new ChooseArray(this));
            AddState((int) StateIDList.ChooseSort, new ChooseSort(this));
            AddState((int) StateIDList.ChooseSortMode, new ChooseSortMode(this));
            AddState((int) StateIDList.ChooseSearch, new ChooseSearch(this));
            AddState((int) StateIDList.ChooseSearchMode, new ChooseSearchMode(this));
            AddState((int) StateIDList.ChooseValue, new ChooseValue(this));
            AddState((int) StateIDList.Merge, new Merge(this));
        }

        private void AddState(int stateID, State state)
        {
            possibleStates.Add(stateID, state);
        }

        public State GetState(int stateID)
        {
            try
            {
                return possibleStates[stateID];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"State with ID {stateID} was not found!");
                return null;
            }
        }

        public void ChangeState(State newState)
        {
            // Check if state we saved - is going to be activated.
            if (nextState != null && nextState == newState)
            {
                nextState = null;
            }

            if (newState != null)
            {
                // Switch to new state.
                currentState = newState;

                // Enter the new state.
                currentState.Enter();
            }
        }

        public void SaveState(State futureState)
        {
            nextState = futureState;
        }
    }
}
