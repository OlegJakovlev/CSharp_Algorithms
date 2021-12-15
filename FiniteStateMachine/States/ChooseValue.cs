using AlgorithmsAssignment.Exceptions;
using System;


namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class ChooseValue : State
    {
        public ChooseValue(FSM fsm) : base(fsm)
        {

        }

        protected int ValidateAndHandleInput()
        {
            string userInput = Console.ReadLine();
            Console.WriteLine("");

            // Validate input with regexp.
            if (!validator.IsMatch(userInput))
            {
                throw new WrongInputException("USER ERROR: Passed wrong option!");
            }

            // Validate based on number of available options.
            int result ;

            try
            {
                result = Int32.Parse(userInput);
            }
            catch (OverflowException)
            {
                result = -1;
            }

            return result;
        }

        public override void Enter()
        {
            int inputValue = -1;

            // Get good input.
            while (inputValue == -1)
            {
                try
                {
                    Console.WriteLine("Please input an integer to find in selected arrays");
                    inputValue = ValidateAndHandleInput();
                }
                catch (WrongInputException)
                {
                    // Handle the exception.
                }
            }

            // Get references to search state and mode state.
            ChooseSearch searchState = (ChooseSearch) currentFSM.GetState((int)FSM.StateIDList.ChooseSearch);
            ChooseSearchMode modeState = (ChooseSearchMode) currentFSM.GetState((int)FSM.StateIDList.ChooseSearchMode);

            // Perform search.
            searchState.applySearch(modeState.mode, inputValue);

            // Go back to menu.
            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.Menu));
        }
    }
}
