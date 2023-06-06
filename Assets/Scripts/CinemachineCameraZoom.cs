using UnityEngine;
using Cinemachine;

public class CinemachineCameraZoom : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 10f;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Update()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        float zoomAmount = scrollWheelInput * zoomSpeed;

        float newZoomDistance = Mathf.Clamp(transposer.m_FollowOffset.z + zoomAmount, minZoomDistance, maxZoomDistance);
       // print(newZoomDistance);
        transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x, transposer.m_FollowOffset.y, newZoomDistance);
    }
}