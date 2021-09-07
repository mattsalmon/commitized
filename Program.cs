using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LibGit2Sharp;
using System.Linq;

namespace commitized
{
    class Program
    {
        static List<string> validTypes = new() {"build", "ci", "docs", "feat", "fix", "perf", "refactor", "style", "test", "chore"};

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

                Console.WriteLine($"dotnet cz v{versionString}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                Console.WriteLine("  dotnet cz <message>");
                return;
            }

            CheckCommitMessagesForType(string.Join(' ', args));
        }

        static void CheckCommitMessagesForType(string message)
        {
            using (var repo = new Repository(Directory.GetCurrentDirectory()))
            {
                var invalidCommits = repo.Commits.Where(c => validTypes.TrueForAll(t=>!c.Message.StartsWith(t)));
                foreach (var commit in invalidCommits)
                {
                    Console.WriteLine(
                        $"Commit id {commit.Id.ToString().Substring(0, 7)} " +
                        $"does not start with a valid type : " +
                        $"{commit.MessageShort}");
                }
            }
        }
    }
}
