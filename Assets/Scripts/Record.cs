using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class Record
{
    [JsonProperty("records")]
    public RecordElement[] Records { get; set; }
}

public partial class RecordElement
{
    [JsonProperty("playerName")]
    public string PlayerName { get; set; }

    [JsonProperty("score")]
    public long Score { get; set; }
}

public partial class Record
{
    public static Record FromJson(string json) => JsonConvert.DeserializeObject<Record>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this Record self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

internal class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
    };
}
