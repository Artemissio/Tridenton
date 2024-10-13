using System.Net;
using Tridenton.Core;
using Tridenton.Core.Utilities;

var error = new Error(HttpStatusCode.NoContent, "Test", "Test Error");

var json = Serializer.ToJson(error);

Console.WriteLine(json);

Console.ReadKey();