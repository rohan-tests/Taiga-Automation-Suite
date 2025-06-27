using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)] // Run feature files in parallel
[assembly: LevelOfParallelism(5)] // Adjust based on your machine or CI capacity