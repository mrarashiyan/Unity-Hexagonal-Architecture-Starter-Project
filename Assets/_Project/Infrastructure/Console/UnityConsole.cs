using System.Diagnostics;
using Project.Application.Ports;
using Debug = UnityEngine.Debug;

namespace Project.Infrastructure.Console
{
    public class UnityConsole : IConsole
    {
        public void Log(string message)
        {
            Debug.Log(FormatCallStack() + message);
        }

        public void LogError(string message)
        {
            Debug.LogError(FormatCallStack() + message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(FormatCallStack() + message);
        }

        private string FormatCallStack()
        {
            var lastCallStack = new StackFrame(1).GetMethod();
            return $"[{lastCallStack.DeclaringType}] {lastCallStack.Name} : ";
        }
    }
}