### Enumeration benchmarks results:

| Method              | index | value |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|---------------------|-------|-------|---------:|--------:|--------:|-------:|----------:|
| GetByIndexLinq      | 1     |       | 151.6 ns | 0.82 ns | 0.77 ns | 0.0477 |     400 B |
| GetByIndexForeach   | 1     |       | 124.9 ns | 0.10 ns | 0.09 ns | 0.0372 |     312 B |
| GetByIndexArrayLoop | 1     |       | 445.8 ns | 0.89 ns | 0.79 ns | 0.0763 |     640 B |
| GetByIndexLinq      | 5     |       | 243.0 ns | 0.19 ns | 0.17 ns | 0.0477 |     400 B |
| GetByIndexForeach   | 5     |       | 223.3 ns | 1.14 ns | 1.01 ns | 0.0372 |     312 B |
| GetByIndexArrayLoop | 5     |       | 434.3 ns | 0.41 ns | 0.37 ns | 0.0763 |     640 B |
| GetByIndexLinq      | 9     |       | 338.0 ns | 0.92 ns | 0.82 ns | 0.0477 |     400 B |
| GetByIndexForeach   | 9     |       | 311.6 ns | 0.21 ns | 0.18 ns | 0.0372 |     312 B |
| GetByIndexArrayLoop | 9     |       | 430.7 ns | 0.46 ns | 0.39 ns | 0.0763 |     640 B |
| GetByValueLinq      |       | Test1 | 143.2 ns | 0.20 ns | 0.18 ns | 0.0477 |     400 B |
| GetByValueForeach   |       | Test1 | 126.0 ns | 0.07 ns | 0.07 ns | 0.0372 |     312 B |
| GetByValueArrayLoop |       | Test1 | 431.4 ns | 0.47 ns | 0.39 ns | 0.0763 |     640 B |
| GetByValueLinq      |       | Test5 | 261.4 ns | 0.20 ns | 0.18 ns | 0.0477 |     400 B |
| GetByValueForeach   |       | Test5 | 243.4 ns | 1.14 ns | 1.01 ns | 0.0372 |     312 B |
| GetByValueArrayLoop |       | Test5 | 467.9 ns | 0.78 ns | 0.73 ns | 0.0763 |     640 B |
| GetByValueLinq      |       | Test9 | 374.4 ns | 0.28 ns | 0.27 ns | 0.0477 |     400 B |
| GetByValueForeach   |       | Test9 | 349.4 ns | 0.40 ns | 0.33 ns | 0.0372 |     312 B |
| GetByValueArrayLoop |       | Test9 | 476.8 ns | 0.49 ns | 0.46 ns | 0.0763 |     640 B |

As we can see from the results, `GetByIndexArrayLoop` / `GetByValueArrayLoop` have the worst performance
in terms of both memory and speed.

The second place is `GetByIndexLinq` / `GetByValueLinq`.

The best performance belongs to **GetByIndexForeach** / **GetByValueForeach**
which is why this implementation is used at `Enumeration.GetValue`