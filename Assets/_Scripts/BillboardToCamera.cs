using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    private Transform cam;

    void Awake()
    {
        if (Camera.main != null)
            cam = Camera.main.transform;
        else
            Debug.LogError("Billboard: Main Camera not found");
    }

    void LateUpdate()
    {
        if (cam == null)
            return;

        transform.LookAt(cam);
        transform.Rotate(0, 180f, 0);
    }
}
