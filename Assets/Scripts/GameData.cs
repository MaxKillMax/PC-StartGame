using System;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using Newtonsoft.Json;
using SG.Dialogs;
using SG.Players;
using UnityEditor;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = nameof(GameData), order = 51)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private TextAsset _playerAsset;
        [SerializeField] private PlayerInfo _playerInfo;

        [Space]

        [SerializeField] private TextAsset _nodesAsset;
        [SerializeField] private DialogsArray _nodes;

        public PlayerInfo PlayerInfo => _playerInfo;
        public List<Dialog> Dialogs => _nodes.Dialogs;

#if UNITY_EDITOR
        [Button("Load")]
        private void TryLoad()
        {
            if (!_nodesAsset)
                return;

            _playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(_playerAsset.text);
            _nodes = JsonConvert.DeserializeObject<DialogsArray>(_nodesAsset.text);
        }

        [Button("Save")]
        private void TrySave()
        {
            if (!_nodesAsset)
                return;

            SavePlayer();
            SaveNodes();
        }

        private void SavePlayer()
        {
            string text = JsonConvert.SerializeObject(_playerInfo);
            string path = Application.dataPath + AssetDatabase.GetAssetPath(_playerAsset).Remove(0, 6);

            Save(text, path);
        }

        private void SaveNodes()
        {
            string text = JsonConvert.SerializeObject(_nodes);
            string path = Application.dataPath + AssetDatabase.GetAssetPath(_nodesAsset).Remove(0, 6);

            Save(text, path);
        }

        private void Save(string text, string path)
        {
            string[] directories = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string currentDirectory = string.Empty;

            for (int i = 0; i < directories.Length - 1; i++)
            {
                directories[i] += '/';
                currentDirectory += directories[i];

                if (!Directory.Exists(currentDirectory))
                    Directory.CreateDirectory(currentDirectory);
            }

            string file = path;

            if (File.Exists(file))
                File.Delete(file);

            File.WriteAllText(file, text);
            AssetDatabase.Refresh();
        }
#endif
    }
}