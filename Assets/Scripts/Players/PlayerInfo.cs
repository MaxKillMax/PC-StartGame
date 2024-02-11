using System.Collections.Generic;
using Newtonsoft.Json;

namespace SG.Players
{
    [System.Serializable]
    public class PlayerInfo
    {
        [JsonProperty("stats")] public List<int> Stats;
        [JsonProperty("staff")] public List<int> Staff;
    }
}
