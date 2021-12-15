using AlgorithmsAssignment.Exceptions;
using System;
using System.Collections.Generic;

namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class ChooseArray : State
    {
        public ChooseArray(FSM fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            int inputValue = -1;

            // Get good input.
            while (inputValue != ReadAndStoreData.dataStorage.Count + 1)
            {
                try
                {
                    PrintArrayOptions();
                    inputValue = ValidateAndHandleInput(ReadAndStoreData.dataStorage.Count + 1);

                    // Working with array, should decrement input.
                    int userInput = --inputValue;

                    // Continue option.
                    if (userInput == ReadAndStoreData.dataStorage.Count)
                    {
                        // If any arrays selected execute next step we previously saved.
                        if (ReadAndStoreData.AnyArraySelected())
                        {
                            currentFSM.ChangeState(currentFSM.nextState);
                            break;
                        }

                        // Return to menu.
                        else
                        {
                            Console.WriteLine("No arrays were selected, back to menu...");
                            currentFSM.SaveState(null);
                            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.Menu));
                            break;
                        }
                    }

                    // Select/Deselect arrays to work with.
                    else if (userInput >= 0 && userInput < ReadAndStoreData.dataStorage.Count)
                    {
                        ReadAndStoreData.dataStorage[ReadAndStoreData.dataStorageKeys[userInput]].active = !ReadAndStoreData.dataStorage[ReadAndStoreData.dataStorageKeys[userInput]].active;
                    }
                    // Wrong input.
                    else
                    {
                        throw new WrongInputException("USER ERROR: Passed wrong option!");
                    }
                }
                catch (WrongInputException)
                {
                    // Handle the exception.
                }
            }
        }

        // Display options dynamically as arrays might change.
        private void PrintArrayOptions()
        {
            Console.WriteLine("Which arrays to activate/disable?");
            int i = 0;
            foreach (var data in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data item = data.Value;
                Console.WriteLine((i + 1) + ". " + (item.active == true ? "[X]" : "[ ]") + " " + data.Key);
                i++;
            }
            Console.WriteLine((i + 1) + ". " + "Continue");
        }
    }
}
