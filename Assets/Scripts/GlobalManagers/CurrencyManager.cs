using System;

namespace GlobalManagers
{
    public static class CurrencyManager
    {
        public static event Action<float> OnCurrencyValueChanged;

        public static float CurrencyValue { get; private set; }

        public static void Initialize(float value) => CurrencyValue = value;

        public static void AddCurrency(float addValue)
        {
            CurrencyValue += addValue;

            OnCurrencyValueChanged?.Invoke(CurrencyValue);
        }
    }
}