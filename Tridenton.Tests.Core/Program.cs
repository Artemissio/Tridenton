using System.Net;
using BenchmarkDotNet.Running;
using Tridenton.Core;
using Tridenton.Core.Utilities;
using Tridenton.Tests.Core;

var benchmark = BenchmarkRunner.Run<EnumerationBenchmarks>();