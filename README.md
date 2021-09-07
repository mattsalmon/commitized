# commitized
Like commitizen, but for dotnet
Has a dependency on the library LibGit2Sharp

## Setup
Commitized is installed using Nuget
> dotnet add package Commitized

In the first version, Commitized is hard-coded to look for the following types in commit messages:
- "build",
- "ci",
- "docs",
- "feat",
- "fix",
- "perf",
- "refactor",
- "style",
- "test",
- "chore"
