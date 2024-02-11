using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SG.Utilities;

namespace SG.Dialogs
{
    [Serializable]
    public class DialogVariant
    {
        [JsonProperty("id")] public int Id;
        [JsonProperty("text")] public string Text;
        [JsonProperty("to")] public int To;
        [JsonProperty("conditions")] public List<SerializedMethod> Conditions;
        [JsonProperty("actions")] public List<SerializedMethod> Actions;
    }
}