using Tridenton.Core.Utilities.Collections;

namespace Tridenton.Tests.Core;

// var properties = new PropertiesCollection();
//
// var test1 = new
// {
//     Id = 1,
//     FirstName = "John",
//     LastName = "Doe",
//     DateOfBirth = DateTime.Today,
//     Email = "john@doe.com",
// };
//
// var test2 = new
// {
//     Id = 2,
//     FirstName = "Sue",
//     LastName = "Storm",
//     DateOfBirth = DateTime.Today,
//     Email = "sue@storm.com",
// };
//
// var testsArray = new[] { test1, test2 };
// var testsList = new List<object> { test1, test2 };
//
// properties.Set("Bool", true);
// properties.Set("Char", 'y');
// properties.Set("String", "string");
// properties.Set("Short", short.MaxValue);
// properties.Set("UnsignedShort", ushort.MaxValue);
// properties.Set("Integer", int.MinValue);
// properties.Set("UnsignedInteger", uint.MaxValue);
// properties.Set("Long", long.MaxValue);
// properties.Set("UnsignedLong", ulong.MaxValue);
// properties.Set("Decimal", 56.7M);
// properties.Set("Double", 56.7D);
// properties.Set("Float", 34564.2435F);
// properties.Set("DateTime", DateTime.UtcNow);
// properties.Set("DateOnly", DateOnly.MaxValue);
// properties.Set("DateTimeOffset", DateTimeOffset.UtcNow);
// properties.Set("TimeSpan", TimeSpan.MaxValue);
// properties.Set("TimeOnly", TimeOnly.MaxValue);
// properties.Set("Guid", Guid.NewGuid());
// properties.Set("Ulid", Ulid.MaxValue);
// properties.Set("Byte", byte.MaxValue);
// properties.Set("Bytes", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
// properties.Set("Enum", PropertyType.Enum);
// properties.Set("Object", test1);
// properties.Set("Array", testsArray);
// properties.Set("List", testsList);
//
// if (properties.TryGet("UnsignedInteger", out uint unsignedInteger))
// {
//     Console.WriteLine(unsignedInteger);
// }
//
// if (properties.TryGet("Bytes", out byte[] bytes))
// {
//     Console.WriteLine(bytes);
// }
//
// if (properties.TryGet("Enum", out PropertyType propertyType))
// {
//     Console.WriteLine(propertyType);
// }
//
// if (properties.TryGet("Array", out object[] array))
// {
//     foreach (var item in array)
//     {
//         Console.WriteLine(item);
//     }
// }
//
// if (properties.TryGet("Object", out object obj))
// {
//     Console.WriteLine(obj);
// }
//
// Console.WriteLine();
//
// Console.ReadKey();