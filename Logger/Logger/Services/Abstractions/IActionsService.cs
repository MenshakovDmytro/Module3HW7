namespace Logger.Services.Abstractions
{
    public interface IActionsService
    {
        public bool WriteInfoLog();
        public bool SkipLogic();
        public bool BreakLogic();
    }
}