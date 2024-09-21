namespace CourseGuide.Objects.Enums.State
{
    public interface IStatusState
    {
        string State { get; }
        bool CanDefaultState();
        bool CanInProgress();
        bool CanFinish();
        bool CanBlock();
    }

    public static class IStatusStateExtensions
    {
        private static IStatusState CreateState(StatusReservation status)
        {
            return status switch
            {
                StatusReservation.OnHold => new OnHoldState(),
                StatusReservation.InProgress => new InProgressState(),
                StatusReservation.Finished => new FinishedState(),
                StatusReservation.Blocked => new BlockedState(),
                _ => new OnHoldState()
            };
        }

        public static string GetState(StatusReservation status)
        {
            IStatusState statusState = CreateState(status);
            return statusState.State;
        }

        public static bool CanDefaultState(StatusReservation status)
        {
            IStatusState statusState = CreateState(status);
            return statusState.CanDefaultState();
        }

        public static bool CanInProgress(StatusReservation status)
        {
            IStatusState statusState = CreateState(status);
            return statusState.CanInProgress();
        }

        public static bool CanFinish(StatusReservation status)
        {
            IStatusState statusState = CreateState(status);
            return statusState.CanFinish();
        }

        public static bool CanBlock(StatusReservation status)
        {
            IStatusState statusState = CreateState(status);
            return statusState.CanBlock();
        }
    }
}
