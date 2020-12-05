using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private static Vector3 vector3Up = Vector3.up;
    private static Vector3 vector3Forward = Vector3.forward;

    [SerializeField] private CameraData cameraData;

    private Quaternion cameraRotation;

    private void Awake()
    {
        cameraRotation = cameraData.Transform.rotation;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cameraRotation * vector3Forward, cameraRotation * vector3Up);
    }
}
