# BenchmarkTests

### GetAllUsers
|        Method |     Mean |    Error |   StdDev |
|-------------- |---------:|---------:|---------:|
|   GetAllUsers | 68.33 ms | 1.293 ms | 1.270 ms |
| GetAllUsersV1 | 61.45 ms | 1.171 ms | 1.439 ms |
| GetAllUsersV2 | 61.60 ms | 1.162 ms | 1.511 ms |
| GetAllUsersV3 | 39.25 ms | 0.777 ms | 0.831 ms |
| GetAllUsersV4 | 39.34 ms | 0.766 ms | 0.679 ms |

### GetSingle
|         Method |     Mean |   Error |  StdDev |
|--------------- |---------:|--------:|--------:|
| GetSingleAsync | 556.8 us | 4.46 us | 4.18 us |
|      GetSingle | 460.2 us | 2.91 us | 2.58 us |
|    GetSingleV1 | 462.8 us | 4.35 us | 4.07 us |
|    GetSingleV2 | 215.0 us | 1.36 us | 1.27 us |
|  GetSingleV2_1 | 222.8 us | 4.26 us | 5.54 us |
|  GetSingleV2_2 | 268.8 us | 5.06 us | 4.49 us |
|    GetSingleV3 | 461.1 us | 2.95 us | 2.76 us |
|    GetSingleV4 | 501.0 us | 3.87 us | 3.62 us |
|    GetSingleV5 | 495.2 us | 4.52 us | 3.77 us |

### Todo
- [ ] Other method tests
- [ ] Dapper tests