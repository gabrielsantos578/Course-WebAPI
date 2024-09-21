namespace CourseGuide.Objects.Enums
{
    public enum StatusReservation
    {
        OnHold = 1,
        InProgress = 2,
        Finished = 3,
        Blocked = 4
    }

    public static class StatusReservationExtensions
    {
        public static StatusReservation DefaultState() => StatusReservation.OnHold;
        public static StatusReservation PutInProgress() => StatusReservation.InProgress;
        public static StatusReservation Finish() => StatusReservation.Finished;
        public static StatusReservation Block() => StatusReservation.Blocked;
    }
}
