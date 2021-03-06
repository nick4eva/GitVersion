﻿using System.Text;

public class ArgumentBuilder
{
    public ArgumentBuilder(string workingDirectory)
    {
        this.workingDirectory = workingDirectory;
    }


    public ArgumentBuilder(string workingDirectory, string exec, string execArgs, string projectFile, string projectArgs, string logFile, bool isTeamCity)
    {
        this.workingDirectory = workingDirectory;
        this.exec = exec;
        this.execArgs = execArgs;
        this.projectFile = projectFile;
        this.projectArgs = projectArgs;
        this.logFile = logFile;
        this.isTeamCity = isTeamCity;
    }

    public ArgumentBuilder(string workingDirectory, string additionalArguments, bool isTeamCity, string logFile)
    {
        this.workingDirectory = workingDirectory;
        this.isTeamCity = isTeamCity;
        this.additionalArguments = additionalArguments;
        this.logFile = logFile;
    }


    public string WorkingDirectory
    {
        get { return workingDirectory; }
    }


    public string LogFile
    {
        get { return logFile; }
    }

    public bool IsTeamCity
    {
        get { return isTeamCity; }
    }


    public override string ToString()
    {
        var arguments = new StringBuilder();

        arguments.AppendFormat("\"{0}\"", workingDirectory);

        if (!string.IsNullOrWhiteSpace(exec))
        {
            arguments.AppendFormat(" /exec \"{0}\"", exec);
        }

        if (!string.IsNullOrWhiteSpace(execArgs))
        {
            arguments.AppendFormat(" /execArgs \"{0}\"", execArgs);
        }

        if (!string.IsNullOrWhiteSpace(projectFile))
        {
            arguments.AppendFormat(" /proj \"{0}\"", projectFile);
        }

        if (!string.IsNullOrWhiteSpace(projectArgs))
        {
            arguments.AppendFormat(" /projargs \"{0}\"", projectArgs);
        }

        arguments.Append(additionalArguments);

        if (!string.IsNullOrWhiteSpace(logFile))
        {
            arguments.AppendFormat(" /l \"{0}\"", logFile);
        }

        return arguments.ToString();
    }


    readonly string additionalArguments;
    readonly string exec;
    readonly string execArgs;
    readonly bool isTeamCity;
    readonly string logFile;
    readonly string projectArgs;
    readonly string projectFile;
    readonly string workingDirectory;
}