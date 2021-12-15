using AlgorithmsAssignment.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class Merge : State
    {
        // Menu options
        private string[] mergeOptions = new string[]
        {
            "Merge 256 length arrays",
            "Merge 2048 length arrays"
        };

        public Merge(FSM fsm) : base(fsm)
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
                    DisplayOptionList(mergeOptions);
                    inputValue = ValidateAndHandleInput(mergeOptions.Length);
                }
                catch (WrongInputException)
                {
                    // Handle the exception.
                }
            }

            switch (inputValue)
            {
                case 1:
                    ReadAndStoreData.MergeArrays("Road_1_256.txt", "Road_3_256.txt");
                    break;
                case 2:
                    ReadAndStoreData.MergeArrays("Road_1_2048.txt", "Road_3_2048.txt");
                    break;
            }

            Console.WriteLine("New array successfully generated!");

            // Go back to menu.
            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.Menu));
        }
    }
}
