using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1; // Assign player1 in the inspector
    public Transform player2; // Assign player2 in the inspector

    public float smoothSpeed = 0.5f; // Adjust this value to change the smoothing of the camera movement
    public Vector3 offset; // Optional offset to adjust camera position relative to the midpoint
    public float minZoom = 5f; // Minimum zoom level
    public float maxZoom = 10f; // Maximum zoom level
    public float zoomLimiter = 2.0f; // Additional zoom out factor to increase viewable area

    void LateUpdate()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2;
        Vector3 desiredPosition = midpoint + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Adjust the camera's zoom based on the distance between the two players
        float distance = (player1.position - player2.position).magnitude;
        float cameraSize = Mathf.Clamp((distance / 2) + zoomLimiter, minZoom, maxZoom);
        Camera.main.orthographicSize = cameraSize;
    }
}
