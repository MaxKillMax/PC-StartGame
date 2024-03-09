using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SG.Dialogs
{
    [Serializable]
    public class DialogNode
    {
        [JsonProperty("id")] public int Id;
        [SerializeField, JsonProperty("text")] private string _text;
        [JsonProperty("variants")] public List<DialogVariant> Variants;

        [JsonIgnore] private string _usingText;
        [JsonIgnore] public string Text 
        { 
            get
            {
                if (string.IsNullOrEmpty( _usingText))
                    _usingText = _text;

                return _usingText;
            } 
            private set => _usingText = value; 
        }

        public void ResetText() => _usingText = _text;

        public void ReplaceIdInText(int id, string text) => Text = Text.Replace("{" + id + "}", text);

        public void ReplaceIdsInOrder(params string[] texts)
        {
            ResetText();

            for (int i = 0; i < texts.Length; i++)
                ReplaceIdInText(i, texts[i]);
        }
    }
}