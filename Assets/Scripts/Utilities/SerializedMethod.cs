using System;
using Newtonsoft.Json;

namespace SG.Utilities
{
    [Serializable]
    public class SerializedMethod
    {
        [JsonProperty("name")] public string Name;
        [JsonProperty("parameters")] public string[] Parameters;
    }
}