namespace CourseGuide.Objects.Enums.State
{
    public class FinishedState : IStatusState
    {
        public string State { get; } = "Finalizada";
        public bool CanDefaultState() => false;
        public bool CanInProgress() => false;
        public bool CanFinish() => false;
        public bool CanBlock() => false;
    }
}
