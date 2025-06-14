using UnityEngine;

public class MenuCameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float rotationSmoothTime = 0.08f;

    [SerializeField] private float maxYaw = 30f;
    [SerializeField] private float maxPitch = 15f; 

    private Vector2 currentRotation;
    private Vector2 targetRotation;
    private Vector2 rotationVelocity;

    private Vector2 centerRotation;

    void Start()
    {
        Vector3 initialEuler = transform.localEulerAngles;
        centerRotation = new Vector2(initialEuler.y, initialEuler.x);
        currentRotation = centerRotation;
        targetRotation = centerRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        targetRotation.x += mouseX;
        targetRotation.y -= mouseY;

        targetRotation.x = Mathf.Clamp(targetRotation.x, centerRotation.x - maxYaw, centerRotation.x + maxYaw);
        targetRotation.y = Mathf.Clamp(targetRotation.y, centerRotation.y - maxPitch, centerRotation.y + maxPitch);

        currentRotation = Vector2.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, rotationSmoothTime);

        transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0f);
    }
}
