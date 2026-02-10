namespace Project.Application.Ports
{
    public interface IConsole
    {
        public void Log(string message);
        public void LogError(string message);
        public void LogWarning(string message);
    }
}