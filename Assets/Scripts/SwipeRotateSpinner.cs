using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class SwipeRotateSpinner : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private Rigidbody rb;
    public float torqueMultiplier = 1000f;
    public float swipeThreshold = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;
            if (swipeDirection.magnitude > swipeThreshold)
            {
                DetermineAndRotate(swipeDirection);
            }
        }
    }

    void DetermineAndRotate(Vector2 swipeDirection)
    {
        Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(startTouchPosition.x, startTouchPosition.y, Camera.main.transform.position.y));

        Vector3 relativeStartPosition = worldStartPosition - transform.position;

        bool isHorizontalSwipe = Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y);

        if (isHorizontalSwipe)
        {
            // Horizontal swipe
            RotateCube(swipeDirection.x > 0
                ? (relativeStartPosition.z > 0 ? Vector3.up : -Vector3.up)
                : (relativeStartPosition.z > 0 ? -Vector3.up : Vector3.up));
        }
        else
        {
            // Vertical swipe
            RotateCube(swipeDirection.y > 0
                ? (relativeStartPosition.x > 0 ? -Vector3.up : Vector3.up)
                : (relativeStartPosition.x > 0 ? Vector3.up : -Vector3.up));
        }
    }

    void RotateCube(Vector3 rotationDirection)
    {
        rb.AddTorque(rotationDirection * torqueMultiplier);
    }
}
