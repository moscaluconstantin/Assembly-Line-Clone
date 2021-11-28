
using Game.Managers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ManagersHolder", menuName = "GameData/ManagersHolder", order = 0)]
    public class ManagersHolder : ScriptableObject
    {
        public BuildManager buildManager;
        public SelectionManager selectionManager;
    }
}