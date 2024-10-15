using BenchmarkDotNet.Running;
using Tridenton.Tests.Core;

// var benchmark = BenchmarkRunner.Run<EnumerationBenchmarks>();
// var benchmark = BenchmarkRunner.Run<GuidVsUlidBenchmarks>();
var benchmark = BenchmarkRunner.Run<ExtendedCollectionsBenchmarks>();
// var benchmark = BenchmarkRunner.Run<GetByKeyBenchmarks>();
