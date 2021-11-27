using UnityEngine;

namespace Game.Controllers.Devices
{
    public abstract class DeviceBaseController : MonoBehaviour
    {
        [SerializeField] private float price;
        [SerializeField] private float sellMultiplier = .8f;

        public float Price => price;
        public float SellPrice => price * sellMultiplier;

        protected bool IsActive;
    }
}