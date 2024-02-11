using SG.UI;
using UnityEngine;

namespace SG.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private AnswersPanel _answersPanel;

        private void Update()
        {
            for (int i = 1; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                    ClickOnNumberButton(i);
            }
        }

        private void ClickOnNumberButton(int number)
        {
            if (number > _answersPanel.AnswerButtons.Count || !_answersPanel.gameObject.activeInHierarchy)
                return;

            _answersPanel.AnswerButtons[number - 1].Click();
        }
    }
}