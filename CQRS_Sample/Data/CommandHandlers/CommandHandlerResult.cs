namespace CQRS_Sample.Data.CommandHandlers
{
    public class CommandHandlerResult
    {
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
    }
}
