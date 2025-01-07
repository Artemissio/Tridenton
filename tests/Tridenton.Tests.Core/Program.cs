using BenchmarkDotNet.Running;
using Tridenton.Core.Utilities;
using Tridenton.Tests.Core;

// _ = BenchmarkRunner.Run<EnumerationBenchmarks>();
// _ = BenchmarkRunner.Run<GuidVsUlidBenchmarks>();
_ = BenchmarkRunner.Run<ExtendedCollectionsBenchmarks>();
// _ = BenchmarkRunner.Run<GetByKeyBenchmarks>();