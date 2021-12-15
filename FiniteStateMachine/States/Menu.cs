using AlgorithmsAssignment.Exceptions;
using System;


namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class Menu : State
    {
        // Menu options
        private string[] mainMenuOptions = new string[]
        {
            "Read files and store the data in arrays",
            "Sort in ascending and descending order and display every 10th/50th value",
            "Search for user-defined value (different modes available)",
            "Merge arrays and perform operations",
            "Exit"
        };

        public Menu(FSM fsm) : base(fsm)
        {
        }

        public override void Enter()
        {
            Console.WriteLine("");

            bool finished = false;

            while (!finished)
            {
                int inputValue = -1;

                // Get good input.
                while (inputValue == -1)
                {
                    try
                    {
                        DisplayOptionList(mainMenuOptions);
                        inputValue = ValidateAndHandleInput(mainMenuOptions.Length);
                    }
                    catch (WrongInputException)
                    {
                        // Handle the exception.
                    }
                }

                try
                {
                    switch (inputValue)
                    {
                        case 1:
                            finished = true;
                            ReadAndStoreData.ReadFiles();
                            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.Menu));
                            break;

                        case 2:
                            if (ReadAndStoreData.DataExists())
                            {
                                finished = true;
                                currentFSM.SaveState(currentFSM.GetState((int)FSM.StateIDList.ChooseSort));
                                currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.ChooseArray));
                            }
                            else
                            {
                                throw new StoredDataIsInvalidException("No data available, please read data from files first!");
                            }
                            break;

                        case 3:
                            if (ReadAndStoreData.DataExists())
                            {
                                finished = true;
                                currentFSM.SaveState(currentFSM.GetState((int)FSM.StateIDList.ChooseSearch));
                                currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.ChooseArray));
                            }
                            else
                            {
                                throw new StoredDataIsInvalidException("No data available, please read data from files first!");
                            }
                            break;

                        case 4:
                            if (ReadAndStoreData.DataExists())
                            {
                                finished = true;
                                currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.Merge));
                            }
                            else
                            {
                                throw new StoredDataIsInvalidException("No data available, please read data from files first!");
                            }
                            break;

                        case 5:
                            finished = true;
                            Console.WriteLine("Thanks for using our software!");
                            break;
                    }
                }
                catch (StoredDataIsInvalidException)
                {
                    // Handle exception.
                }
            }
        }
    }
}
