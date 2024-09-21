namespace CourseGuide.Objects.Enums.State
{
    public class OnHoldState : IStatusState
    {
        public string State { get; } = "Em Espera";
        public bool CanDefaultState() => false;
        public bool CanInProgress() => true;
        public bool CanFinish() => false;
        public bool CanBlock() => true;
    }
}
