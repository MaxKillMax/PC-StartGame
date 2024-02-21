using UnityEngine;

namespace SG.Interactables
{
    public class LogInteractive : Interactive
    {
        [SerializeField] private string _log;

        protected override void DoAction()
        {
            GameLog.Log(_log);
        }
    }
}