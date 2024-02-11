using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Location = SG.Locations.Location;

namespace SG.Dialogs
{
    [Serializable]
    public class Dialog
    {
        [JsonProperty("id")] public int Id;

        [JsonIgnore, SerializeField] public Location Prefab;

        private string _assetPath;
#if UNITY_EDITOR
        [JsonProperty("assetPath")]
#pragma warning disable IDE0051
        private string AssetPath
#pragma warning restore IDE0051
        {
            get => Prefab ? AssetDatabase.GetAssetPath(Prefab) : string.Empty; set
            {
                _assetPath = value;

                if (!string.IsNullOrEmpty(_assetPath))
                    Prefab = AssetDatabase.LoadAssetAtPath<Location>(value);
            }
        }
#endif

        [JsonProperty("dialogs")] public DialogNode[] Nodes;
    }
}