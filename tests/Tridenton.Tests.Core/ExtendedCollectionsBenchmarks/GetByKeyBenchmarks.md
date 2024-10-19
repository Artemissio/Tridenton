### Get by key benchmarks results:

| Method       |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|--------------|---------:|--------:|--------:|-------:|----------:|
| Guid         | 240.3 ns | 0.38 ns | 0.32 ns | 0.0162 |     136 B |
| GuidForeach  | 151.7 ns | 0.07 ns | 0.05 ns |      - |         - |
| Ulid         | 239.1 ns | 0.22 ns | 0.21 ns | 0.0162 |     136 B |
| UlidForeach  | 139.5 ns | 0.11 ns | 0.09 ns |      - |         - |
| Index        | 266.7 ns | 0.18 ns | 0.17 ns | 0.0153 |     128 B |
| IndexForeach | 135.2 ns | 0.33 ns | 0.28 ns |      - |         - |