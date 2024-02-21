using UnityEngine;
using UnityEngine.UI;

namespace SG.Interactables
{
    [RequireComponent(typeof(Button))]
    public abstract class Interactive : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(DoAction);
        }

        protected abstract void DoAction();
    }
}