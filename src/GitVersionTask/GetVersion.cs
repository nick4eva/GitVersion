﻿namespace GitVersionTask
{
    using System;
    using GitVersion;
    using GitVersion.Helpers;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Logger = GitVersion.Logger;

    public class GetVersion : Task
    {
        [Required]
        public string SolutionDirectory { get; set; }

        public bool NoFetch { get; set; }

        [Output]
        public string Major { get; set; }

        [Output]
        public string Minor { get; set; }

        [Output]
        public string Patch { get; set; }

        [Output]
        public string PreReleaseTag { get; set; }

        [Output]
        public string PreReleaseTagWithDash { get; set; }

        [Output]
        public string BuildMetaData { get; set; }

        [Output]
        public string FullBuildMetaData { get; set; }

        [Output]
        public string MajorMinorPatch { get; set; }

        [Output]
        public string SemVer { get; set; }

        [Output]
        public string LegacySemVer { get; set; }

        [Output]
        public string LegacySemVerPadded { get; set; }

        [Output]
        public string AssemblySemVer { get; set; }

        [Output]
        public string FullSemVer { get; set; }

        [Output]
        public string InformationalVersion { get; set; }

        [Output]
        public string BranchName { get; set; }

        [Output]
        public string Sha { get; set; }

        [Output]
        public string NuGetVersionV2 { get; set; }

        [Output]
        public string NuGetVersion { get; set; }

        [Output]
        public string CommitDate { get; set; }

        TaskLogger logger;
        IFileSystem fileSystem;

        public GetVersion()
        {
            logger = new TaskLogger(this);
            fileSystem = new FileSystem();
            Logger.SetLoggers(
                this.LogInfo,
                this.LogWarning,
                s => this.LogError(s));
        }

        public override bool Execute()
        {
            try
            {
                VersionVariables variables;

                if (VersionAndBranchFinder.TryGetVersion(SolutionDirectory, out variables, NoFetch, new Authentication(), fileSystem))
                {
                    var thisType = typeof(GetVersion);
                    foreach (var variable in variables)
                    {
                        thisType.GetProperty(variable.Key).SetValue(this, variable.Value, null);
                    }
                }
                return true;
            }
            catch (WarningException errorException)
            {
                logger.LogWarning(errorException.Message);
                return true;
            }
            catch (Exception exception)
            {
                logger.LogError("Error occurred: " + exception);
                return false;
            }
            finally
            {
                Logger.Reset();
            }
        }
    }
}