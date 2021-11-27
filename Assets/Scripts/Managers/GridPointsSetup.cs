using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class GridPointsSetup : MonoBehaviour
    {
        [SerializeField] private int2 gridSize;
        [SerializeField] private int2 spacing;

        public int GridElementsAmount => gridSize.x * gridSize.y;
    
        private Vector3[] gridPoints;

        [Button("Draw Points")]
        public void DrawPoints()
        {
            gridPoints = GetGridPositions();
        }

        public Vector3[] GetGridPositions()
        {
            var positions = new Vector3[GridElementsAmount];

            var startPositionOffset = new Vector3(-gridSize.x * spacing.x / 2f + spacing.x / 2f, gridSize.y * spacing.y / 2f - spacing.y / 2f, 0f);
            var startPosition = transform.position + startPositionOffset;

            for (var i = 0; i < gridSize.y; i++)
            {
                float yOffset = i * -spacing.y;

                for (var j = 0; j < gridSize.x; j++)
                {
                    float xOffset = j * spacing.x;

                    positions[i * gridSize.x + j] = startPosition + new Vector3(xOffset, yOffset,0f);
                }
            }

            return positions;
        }

        private void OnDrawGizmos()
        {
            if (gridPoints == null)
                return;

            Gizmos.color = Color.white;
            var size = new Vector3(spacing.x, spacing.y, 0);

            foreach (var gridPoint in gridPoints)
            {
                Gizmos.DrawWireCube(gridPoint, size);
            }
        }
    }
}