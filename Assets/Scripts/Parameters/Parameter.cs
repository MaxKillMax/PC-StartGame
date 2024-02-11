using System;
using System.Collections.Generic;
using SG.Dialogs;

namespace SG.Parameters
{
    public class Parameter
    {
        private static readonly List<string> Parameters = new();

        public static event Action<string> OnParameterAdded;

        public static bool StateOf(string parameterName) => Parameters.Contains(parameterName);

        public static void Enable(string parameterName)
        {
            if (Parameters.Contains(parameterName))
                return;

            Parameters.Add(parameterName);
            OnParameterAdded?.Invoke(parameterName);
        }

        public static void EnableNode(DialogNode node) => Enable(nameof(DialogNode) + node.Id.ToString());

        public static void EnableVariant(DialogVariant variant) => Enable(nameof(DialogVariant) + variant.Id.ToString());
    }
}