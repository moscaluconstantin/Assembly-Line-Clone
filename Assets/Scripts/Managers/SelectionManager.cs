using NaughtyAttributes;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Color buildSelectionColor;
    [SerializeField] private Color deleteSelectionColor;

    [HorizontalLine] [SerializeField] private Camera gameCamera;

    private void Update()
    {
        Vector2 inputPosition;

#if UNITY_EDITOR
        
        if (!Input.GetMouseButtonDown(0))
            return;

        inputPosition = Input.mousePosition;
        
#elif UNITY_ANDROID

        if (Input.touchCount == 0)
            return;
        
        if (Input.GetTouch(0).phase != TouchPhase.Began)
            return;

        inputPosition = Input.GetTouch(0).position;

#endif


        var touchWorldPosition = gameCamera.ScreenToWorldPoint(inputPosition);
        var touchPosition = new Vector2(touchWorldPosition.x, touchWorldPosition.y);

        var hit = Physics2D.Raycast(touchPosition, Vector2.zero, 20f);

        if (!hit.collider)
            return;

        if (hit.collider.transform.TryGetComponent(out ColorController colorController))
        {
            colorController.SetColor(deleteSelectionColor);
        }
    }
}