using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SG.Dialogs
{
    [Serializable]
    public class DialogsArray
    {
        [JsonProperty("nodes")] public List<Dialog> Dialogs;
    }
}