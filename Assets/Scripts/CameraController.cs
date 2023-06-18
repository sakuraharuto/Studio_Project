using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float boundaryDistanceX = 2f;
    public float boundaryDistanceZ = 3f;
    public float springiness = 5f;
    public float zoomSpeed = 5f;
    public float minZoomDistance = 5f;
    public float maxZoomDistance = 20f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float currentZoomDistance = 10f;

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        targetPosition = originalPosition + moveDirection * moveSpeed;

        targetPosition.x = Mathf.Clamp(targetPosition.x, originalPosition.x - boundaryDistanceX, originalPosition.x + boundaryDistanceX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, originalPosition.z - boundaryDistanceZ, originalPosition.z + boundaryDistanceZ);

        

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / springiness);
    }
}
