using System.Collections.Generic;
using System.Linq;
using Controllers;
using NaughtyAttributes;
using UnityEngine;

namespace Managers
{
    public class DeviceHoldersManager : MonoBehaviour
    {
        [SerializeField] private DeviceHolderController deviceHolderPrefab;
        [SerializeField] private Transform deviceHoldersContainerTransform;

        private GridPointsSetup gridPointsSetup;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            gridPointsSetup = GetComponent<GridPointsSetup>();
            CreateDeviceHolders();
        }

        private void CreateDeviceHolders()
        {
            var deviceHolders = GetMatchToAmountDeviceHolders();
            var points = gridPointsSetup.GetGridPositions();
        
            for (var i = 0; i < deviceHolders.Length; i++)
            {
                deviceHolders[i].transform.position = points[i];
                deviceHolders[i].transform.name = "Device Holder " + i;
            }
        }

        private DeviceHolderController[] GetMatchToAmountDeviceHolders()
        {
            var points = new List<DeviceHolderController>(GetDeviceHolders());

            while (points.Count != gridPointsSetup.GridElementsAmount)
            {
                if (points.Count < gridPointsSetup.GridElementsAmount)
                {
                    var point = Instantiate(deviceHolderPrefab, deviceHoldersContainerTransform);
                    points.Add(point);
                }
                else if (points.Count > gridPointsSetup.GridElementsAmount)
                {
                    var point = points.Last();
                    points.Remove(point);
                    DestroyImmediate(point.gameObject);
                }
            }

            return points.ToArray();
        }

        private IEnumerable<DeviceHolderController> GetDeviceHolders() => deviceHoldersContainerTransform.GetComponentsInChildren<DeviceHolderController>(true);
    
        [Button("Create")]
        private void Create()
        {
            gridPointsSetup = GetComponent<GridPointsSetup>();
            CreateDeviceHolders();
        }
    
        [Button("Remove")]
        private void Remove()
        {
            var points = GetDeviceHolders();

            foreach (var point in points)
            {
                DestroyImmediate(point.gameObject);
            }
        }
    }
}