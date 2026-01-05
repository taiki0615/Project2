using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    private bool hasEnteredScreen = false;

    void Update()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 vp = cam.WorldToViewportPoint(transform.position);

        bool isInside =
            vp.z > 0 &&
            vp.x >= 0 && vp.x <= 1 &&
            vp.y >= 0 && vp.y <= 1;

        if (isInside)
        {
            hasEnteredScreen = true;
        }
        else if (hasEnteredScreen)
        {
            Destroy(transform.root.gameObject);
        }
    }
}
