using AlgorithmsAssignment.Exceptions;
using System;

namespace AlgorithmsAssignment.FiniteStateMachine.States
{
    class ChooseSort : State
    {

        private int selectedSort = -1;
        private string[] sortOptions = new string[]
        {
            "Merge Sort",
            "Heap Sort",
            "Quick Sort",
            "Radix Sort"
        };

        public ChooseSort(FSM fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            selectedSort = -1;

            // Get good input.
            while (selectedSort == -1)
            {
                try
                {
                    DisplayOptionList(sortOptions);
                    selectedSort = ValidateAndHandleInput(sortOptions.Length);
                }
                catch (WrongInputException)
                {
                    // Handle the exception.
                }
            }

            currentFSM.ChangeState(currentFSM.GetState((int)FSM.StateIDList.ChooseSortMode));
            return;
        }

        public void applySort(int sortMode)
        {
            // For ascending and descending modes.
            switch (selectedSort)
            {
                case 1:
                    Sorting.MergeSort(sortMode);
                    break;

                case 2:
                    Sorting.HeapSort(sortMode);
                    break;

                case 3:
                    Sorting.QuickSort(sortMode);
                    break;

                case 4:
                    Sorting.RadixSort(sortMode);
                    break;
            }
        }
    }
}
