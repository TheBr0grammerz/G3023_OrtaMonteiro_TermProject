using UnityEngine;
using System.IO;

public class ConsoleLogger : MonoBehaviour
{
    private StreamWriter logWriter;

    void Awake()
    {
        // Open or create the log file
        logWriter = new StreamWriter("console_log.txt", true);
        logWriter.AutoFlush = true;

        // Subscribe to log events
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logWriter.WriteLine(logString);
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
        logWriter.Close();
    }
}