using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private float rotationSmoothTime = 0.1f;

    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private Vector2 currentRotation;
    private Vector2 targetRotation;
    private Vector2 rotationVelocity;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        targetRotation.x += mouseX;
        targetRotation.y -= mouseY;

        targetRotation.y = Mathf.Clamp(targetRotation.y, minPitch, maxPitch);

        currentRotation = Vector2.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, rotationSmoothTime);

        transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0f);
    }
}
