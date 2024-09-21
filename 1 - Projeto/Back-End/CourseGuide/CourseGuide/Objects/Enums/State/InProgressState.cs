namespace CourseGuide.Objects.Enums.State
{
    public class InProgressState : IStatusState
    {
        public string State { get; } = "Em Progresso";
        public bool CanDefaultState() => false;
        public bool CanInProgress() => false;
        public bool CanFinish() => true;
        public bool CanBlock() => true;
    }
}
