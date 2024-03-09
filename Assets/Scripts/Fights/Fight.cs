using System;
using System.Collections;
using SG.UI;
using SG.Units;
using SG.Units.Players;
using UnityEngine;
using UnityEngine.Assertions;

namespace SG.Fights
{
    public class Fight : MonoBehaviour
    {
        private static Fight Instance;

        [SerializeField] private DialogPanel _defaultDialogPanel;
        [SerializeField] private FightPanel _fightPanel;

        private readonly static int CombatTypesCount;

        private static CombatUnit Ally;
        private static CombatUnit Enemy;

        public static bool InProcess { get; private set; }
        private static bool IsAllyTurn = true;
        private static CombatUnit Attacker => IsAllyTurn ? Ally : Enemy;
        private static CombatUnit Defender => IsAllyTurn ? Enemy : Ally;

        public static event Action OnFailed;
        public static event Action OnEnded;

        static Fight()
        {
            CombatTypesCount = Enum.GetValues(typeof(CombatType)).Length;
        }

        private void Awake()
        {
            Assert.IsNull(Instance);

            Instance = this;
        }

        public static void Start(IUnit ally, IUnit enemy)
        {
            InProcess = true;

            Ally = new(ally);
            Enemy = new(enemy);

            Instance._fightPanel.Init(Ally.Unit, Enemy.Unit);

            SetActiveState(true);
            StartAttack();
        }

        public static void End()
        {
            if (!InProcess)
                return;

            InProcess = false;

            SetActiveState(false);
            OnEnded?.Invoke();
        }

        private static void SetActiveState(bool state)
        {
            Instance._defaultDialogPanel.gameObject.SetActive(!state);
            Instance._fightPanel.gameObject.SetActive(state);
        }

        private static void StartAttack()
        {
            Instance.StartCoroutine(WaitForAttackPicks(() => Instance._fightPanel.WaitForAttackSelection(), AddCombat, StartSeries));
        }

        private static void StartDefend()
        {
            int picksCount = Stat.GetStepsCount(Defender.Unit.Agility);
            Instance.StartCoroutine(WaitForAttackPicks(() => Instance._fightPanel.WaitForDefendSelection(), AddCombat, StartSeries, () => Defender.Series.Count < picksCount));
        }

        private static void AddCombat()
        {
            Ally.Series.Add(Instance._fightPanel.SelectedCombatType);
        }

        private static void StartSeries()
        {
            GenerateEnemyCombat();
            Instance.StartCoroutine(WaitForAttacks(OnSeriesEnded));
        }
        
        private static void OnSeriesEnded()
        {
            if (TryStop())
                return;

            Attacker.Series.Clear();
            Defender.Series.Clear();

            IsAllyTurn = !IsAllyTurn;

            if (IsAllyTurn)
                StartAttack();
            else
                StartDefend();
        }

        private static bool TryStop()
        {
            if (Defender.Unit.Health > 0)
                return false;

            if (IsAllyTurn)
            {
                GameLog.Log("Вы смогли одолеть противника!");

                End();
            }
            else
            {
                GameLog.Log("Вы проиграли в бою!");

                Instance._fightPanel.ShowDeathMessage();
                OnFailed?.Invoke();
            }

            return true;
        }

        private static IEnumerator WaitForAttackPicks(Func<IEnumerator> getEnumerator, Action onIterationEnded, Action onCompleted, Func<bool> condition = null)
        {
            int attackCount = Stat.GetStepsCount(Attacker.Unit.Agility);

            while (attackCount > 0)
            {
                if (condition != null && !condition.Invoke())
                    yield break;

                yield return Instance.StartCoroutine(getEnumerator.Invoke());
                yield return new WaitForEndOfFrame();

                attackCount--;

                onIterationEnded?.Invoke();
            }

            onCompleted.Invoke();
        }

        private static IEnumerator WaitForAttacks(Action onCompleted)
        {
            GameLog.Log("Атакует " + Attacker.Unit.Name);

            for (int i = 0; i < Attacker.Series.Count; i++)
            {
                CombatType defenderCombatType = Defender.Series.Count > i ? Defender.Series[i] : CombatType.None;
                int damage = DamageCalculator.GetDamage(Attacker.Unit, Attacker.Series[i], defenderCombatType);
                bool isMissed = IsMissed(Attacker.Unit);

                if (isMissed)
                {
                    GameLog.Log("Промах");

                    yield return Instance.StartCoroutine(Instance._fightPanel.WaitForMissShowing(Attacker.Unit.Name, Defender.Unit.Name));
                }
                else
                {
                    GameLog.Log($"Нанесение {damage} урона");

                    Defender.Unit.Health -= damage;
                    Instance._fightPanel.UpdateStats();

                    yield return Instance.StartCoroutine(Instance._fightPanel.WaitForAttackShowing(Attacker.Unit.Name, Defender.Unit.Name, damage));

                    if (Defender.Unit.Health <= 0)
                        break;
                }

            }

            onCompleted?.Invoke();
        }

        private static bool IsMissed(IUnit unit)
        {
            int random = UnityEngine.Random.Range(0, 100);
            return Stat.GetMissChance(unit.Lucky) >= random;
        }

        private static void GenerateEnemyCombat()
        {
            Enemy.Series.Clear();
            int stepCount = Stat.GetStepsCount(Enemy.Unit.Agility);

            for (int i = 0; i < stepCount; i++)
            {
                int random = UnityEngine.Random.Range(0, CombatTypesCount);
                Enemy.Series.Add((CombatType)random);
            }
        }
    }
}
