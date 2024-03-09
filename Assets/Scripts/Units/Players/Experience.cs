using System;

namespace SG.Units.Players
{
    public class Experience
    {
        public const int MAX_VALUE = 100;

        private int _value = 0;
        public int Value 
        { 
            get => _value; set
            {
                if (_value == value)
                    return;
                else if (value < 0)
                    value = 0;

                if (value >= MAX_VALUE)
                {
                    _value = 0;
                    OnMaxReached?.Invoke();
                }
                else
                {
                    _value = value;
                }

                OnUpdated?.Invoke();

            }
        }

        public event Action OnUpdated;
        public event Action OnMaxReached;
    }
}
