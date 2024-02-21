using System.Collections.Generic;
using System.Linq;
using SG.Dialogs;
using SG.Locations;
using SG.Parameters;
using SG.Players;
using SG.UI;
using UnityEngine;

namespace SG
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private DialogPanel _dialogPanel;
        [SerializeField] private LocationBuilder _locationBuilder;

        public static Player Player { get; private set; }

        private List<Dialog> _dialogs;

        private Dialog _dialog;
        private DialogNode _node;

        public void Init(List<Dialog> dialogs, Player player)
        {
            Player = player;
            _dialogs = dialogs;
            StartGame();
        }

        public void StartGame()
        {
            GoToDialog(0);
        }

        public void CompleteDialog(DialogVariant variant)
        {
            GameLog.Log(variant.Text);
            Parameter.EnableVariant(variant);

            GoToDialog(variant.To);
        }

        public void GoToDialog(int id)
        {
            Dialog dialog = _dialogs.FirstOrDefault(n => n.Nodes.FirstOrDefault(d => d.Id == id) != null);

            if (dialog == null)
                return;

            _dialog = dialog;
            _node = _dialog.Nodes.First(d => d.Id == id);

            _locationBuilder.Build(_dialog.Prefab);

            GameLog.Log(_node.Text);
            Parameter.EnableNode(_node);

            _dialogPanel.InitAsync(_node, CompleteDialog);
        }

        // Нечего писать в Win и Lose. Все сцены, где можно проиграть или победить итак прямо указываются в тексте панели диалога

        public void Win()
        {
            Debug.Log("Win");
        }

        public void Lose()
        {
            Debug.Log("Lose");
        }

        public void DisableDialogPanel()
        {
            _dialogPanel.gameObject.SetActive(false);
        }
    }
}