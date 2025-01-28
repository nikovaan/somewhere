#pragma once
#include <ctime>
#include <time.h>
#include <fstream>
#include <iostream>
#include <sstream>

// Enum to represent log levels
enum LogLevel { DEBUG, INFO, WARNING, ERROR, CRITICAL };

class ErrorLogger
{
public:
    // Constructor: Opens the log file in append mode
    ErrorLogger(const std::string& filename)
    {
        logFile.open(filename, std::ios::app);
        if (!logFile.is_open()) {
            std::cerr << "Error opening log file." << std::endl;
            std::abort();
        }
    }

    // Destructor: Closes the log file
    ~ErrorLogger() { logFile.close(); }

    // Logs a message with a given log level
    void log(LogLevel level, const std::string& message)
    {
        // Get current timestamp
        struct tm timeinfo;
        errno_t errTime;
        time_t now = time(0);
        errTime = localtime_s(&timeinfo, &now);
        if (errTime)
        {
            std::cerr << "Error obtaining local time." << std::endl;
        }
        char timestamp[20];
        strftime(timestamp, sizeof(timestamp),
            "%Y-%m-%d %H:%M:%S", &timeinfo);

        // Create log entry
        std::ostringstream logEntry;
        logEntry << "[" << timestamp << "] "
            << levelToString(level) << ": " << message
            << std::endl;

        // Output to console
        //std::cout << logEntry.str();

        // Output to log file
        if (logFile.is_open())
        {
            logFile << logEntry.str();
            logFile
                .flush(); // Ensure immediate write to file
        }
    }

private:
    std::ofstream logFile; // File stream for the log file

    // Converts log level to a string for output
    std::string levelToString(LogLevel level)
    {
        switch (level) {
        case DEBUG:
            return "DEBUG";
        case INFO:
            return "INFO";
        case WARNING:
            return "WARNING";
        case ERROR:
            return "ERROR";
        case CRITICAL:
            return "CRITICAL";
        default:
            return "UNKNOWN";
        }
    }
};