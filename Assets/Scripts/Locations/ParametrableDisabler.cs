using System;
using SG.Parameters;
using UnityEngine;

namespace SG.Locations
{
    public class ParametrableDisabler : MonoBehaviour
    {
        [SerializeField] private string[] _parametersToDisable;

        private void Awake()
        {
            if (ActParameters((s) => Parameter.StateOf(s)))
                gameObject.SetActive(false);
            else
                Parameter.OnParameterAdded += UpdateState;
        }

        private void OnDestroy()
        {
            Parameter.OnParameterAdded -= UpdateState;
        }

        private void UpdateState(string parameterName)
        {
            if (!ActParameters((s) => s == parameterName))
                return;

            gameObject.SetActive(false);
            Parameter.OnParameterAdded -= UpdateState;
        }

        private bool ActParameters(Func<string, bool> func)
        {
            for (int i = 0; i < _parametersToDisable.Length; i++)
            {
                if (func.Invoke(_parametersToDisable[i]))
                    return true;
            }

            return false;
        }
    }
}
