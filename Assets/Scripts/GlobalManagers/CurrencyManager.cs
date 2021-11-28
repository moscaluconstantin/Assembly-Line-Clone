using System;
using UnityEngine;

namespace GlobalManagers
{
    public static class CurrencyManager
    {
        public static event Action<float> OnCurrencyValueChanged;
        public static event Action<float> OnBufferCurrencyValueChanged;

        public static float CurrencyValue { get; private set; }

        public static float BufferCurrencyValue
        {
            get => bufferCurrencyValue;
            set
            {
                bufferCurrencyValue = value;
                
                OnBufferCurrencyValueChanged?.Invoke(bufferCurrencyValue);
            }
        }

        private static float bufferCurrencyValue;

        public static void Initialize(float value)
        {
            CurrencyValue = value;
            BufferCurrencyValue = 0;
        }

        public static void AddCurrency(float addValue)
        {
            CurrencyValue += addValue;

            OnCurrencyValueChanged?.Invoke(CurrencyValue);
        }

        public static void FinishTransaction()
        {
            AddCurrency(BufferCurrencyValue);
            BufferCurrencyValue = 0;
        }
    }
}