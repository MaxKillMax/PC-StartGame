using UnityEngine;

namespace Interactable
{
    public abstract class Interactive : MonoBehaviour
    {
        private void OnMouseUp()
        {
            DoAction();
        }

        protected abstract void DoAction();
    }
}