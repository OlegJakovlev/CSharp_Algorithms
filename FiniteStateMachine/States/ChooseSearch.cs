using AlgorithmsAssignment.Exceptions;
using System;

namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class ChooseSearch : State
    {
        public int selectedSearch;

        private string[] searchOptions = new string[]
        {
            "Binary Search",
            "Interpolation Search"
        };

        public ChooseSearch(FSM fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            // Check if selected arrays are sorted, if not go to sort menu.
            if (!ReadAndStoreData.SelectedArraysAreSorted())
            {
                Console.WriteLine("Some of selected arrays are not sorted! Please sort them to use search function!");
                Console.WriteLine("");
                currentFSM.SaveState(currentFSM.currentState);
                currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.ChooseSort));
                return;
            }

            int inputValue = -1;

            // Get good input.
            while (inputValue == -1)
            {
                try
                {
                    DisplayOptionList(searchOptions);
                    inputValue = ValidateAndHandleInput(searchOptions.Length);
                }
                catch (WrongInputException)
                {
                    // Handle the exception.
                }
            }

            // Save selected search.
            selectedSearch = inputValue;

            // Select the mode.
            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.ChooseSearchMode));
        }

        public void applySearch(int mode, int value)
        {
            // Choose search type.
            switch (selectedSearch)
            {
                case 1:
                    Search.BinarySearch(mode, value);
                    break;

                case 2:
                    Search.InterpolationSearch(mode, value);
                    break;
            }
        }
    }
}
