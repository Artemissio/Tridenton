### Extended collections benchmarks results:

| Method            | ItemsCount |     Mean |    Error |    StdDev |   Gen0 |   Gen1 | Allocated |
|-------------------|------------|---------:|---------:|----------:|-------:|-------:|----------:|
| PopulateGuidList  | 1          | 321.8 ns |  1.73 ns |   1.44 ns | 0.0124 | 0.0038 |     104 B |
| PopulateUlidList  | 1          | 232.6 ns | 36.80 ns | 108.50 ns | 0.0124 | 0.0031 |     104 B |
| PopulateIndexList | 1          |       NA |       NA |        NA |     NA |     NA |        NA |