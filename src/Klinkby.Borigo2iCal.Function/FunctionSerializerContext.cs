using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Klinkby.Borigo2iCal.Func;

[JsonSerializable(typeof(ApiClientOptions))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(ContentResult))]
internal sealed partial class FunctionSerializerContext : JsonSerializerContext
{
}
