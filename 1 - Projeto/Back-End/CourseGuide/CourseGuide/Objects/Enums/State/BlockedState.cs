namespace CourseGuide.Objects.Enums.State
{
    public class BlockedState : IStatusState
    {
        public string State { get; } = "Bloqueada";
        public bool CanDefaultState() => false;
        public bool CanInProgress() => false;
        public bool CanFinish() => false;
        public bool CanBlock() => false;
    }
}
