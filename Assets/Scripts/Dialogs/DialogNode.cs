using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SG.Dialogs
{
    [Serializable]
    public class DialogNode
    {
        [JsonProperty("id")] public int Id;
        [JsonProperty("text")] public string Text;
        [JsonProperty("variants")] public List<DialogVariant> Variants;
    }
}