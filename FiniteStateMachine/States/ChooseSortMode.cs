using AlgorithmsAssignment.Exceptions;
using System;

namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class ChooseSortMode : State
    {

        private string[] modeOptions = new string[]
        {
            "Sort ascending",
            "Sort descending"
        };

        public ChooseSortMode(FSM fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            int inputValue = -1;

            // Get good input.
            while (inputValue == -1)
            {
                try
                {
                    DisplayOptionList(modeOptions);
                    inputValue = ValidateAndHandleInput(modeOptions.Length);
                }
                catch (WrongInputException)
                {
                    // Handle the exception.
                }
            }

            // Apply sorting method with mode selected.
            ChooseSort sortState = (ChooseSort) currentFSM.GetState((int)FSM.StateIDList.ChooseSort);
            sortState.applySort(inputValue);

            // Check if we got in sorting from searching.
            if (currentFSM.nextState != null)
            {
                currentFSM.ChangeState(currentFSM.nextState);
            }
            else
            {
                currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.Menu));
            }
        }
    }
}
