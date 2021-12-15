using AlgorithmsAssignment.Exceptions;


namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class ChooseSearchMode : State
    {
        public int mode;

        private string[] modeOptions = new string[]
        {
            "Find user-defined value or error if no such exist",
            "Find user-defined value or nearest value if no such exist"
        };

        public ChooseSearchMode(FSM fsm) : base(fsm) 
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

            mode = inputValue;

            // Select the value.
            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.ChooseValue));
        }
    }
}
