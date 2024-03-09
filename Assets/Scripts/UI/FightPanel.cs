using System;
using System.Collections;
using SG.Dialogs;
using SG.Fights;
using SG.Units;
using UnityEngine;

namespace SG.UI
{
    public class FightPanel : MonoBehaviour
    {
        public const float READ_TIME = 0.5f;

        [SerializeField] private FightStatsPanel _allyStatsPanel;
        [SerializeField] private FightStatsPanel _enemyStatsPanel;

        [SerializeField] private DialogPanel _dialogPanel;

        [Space]

        [SerializeField] private DialogNode _deathNode;

        [SerializeField] private DialogNode _attackSelectionNode;
        [SerializeField] private DialogNode _defendSelectionNode;

        [SerializeField] private DialogNode _missNode;
        [SerializeField] private DialogNode _attackNode;

        private IUnit _ally;
        private IUnit _enemy;

        public CombatType SelectedCombatType { get; private set; } 

        public void Init(IUnit ally, IUnit enemy)
        {
            _ally = ally;
            _allyStatsPanel.SetUnit(_ally);
            _enemy = enemy;
            _enemyStatsPanel.SetUnit(_enemy);
        }

        public void UpdateStats()
        {
            _allyStatsPanel.UpdateStats();
            _enemyStatsPanel.UpdateStats();
        }

        public void ShowDeathMessage()
        {
            _dialogPanel.Clear();
            _dialogPanel.Init(_deathNode);
        }

        public IEnumerator WaitForAttackSelection()
        {
            yield return WaitForSelect(_attackSelectionNode);
        }

        public IEnumerator WaitForDefendSelection()
        {
            yield return WaitForSelect(_defendSelectionNode);
        }

        private IEnumerator WaitForSelect(DialogNode node)
        {
            bool selected = false;

            _dialogPanel.Clear();
            yield return _dialogPanel.Init(node, (variant) =>
            {
                selected = true;
                SelectedCombatType = (CombatType)variant.Id;
                _dialogPanel.Clear();
            });

            while (!selected)
                yield return new WaitForEndOfFrame();
        }

        public IEnumerator WaitForMissShowing(string attackerName, string defenderName)
        {
            yield return PlayNodeWithTextReplacing(_missNode, attackerName, defenderName);
        }

        public IEnumerator WaitForAttackShowing(string attackerName, string defenderName, int damage)
        {
            yield return PlayNodeWithTextReplacing(_attackNode, attackerName, defenderName, damage.ToString());
        }

        private IEnumerator PlayNodeWithTextReplacing(DialogNode node, params string[] texts)
        {
            node.ReplaceIdsInOrder(texts);
            yield return PlayNode(node);
        }

        private IEnumerator PlayNode(DialogNode node)
        {
            _dialogPanel.Clear();
            yield return _dialogPanel.Init(node);
            yield return new WaitForSeconds(READ_TIME);
        }
    }
}