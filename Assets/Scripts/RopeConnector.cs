using UnityEngine;

public class RopeConnector : MonoBehaviour
{
    public Transform connectedPlayer; // Assign the other player in the inspector
    public float maxRopeLength = 5f; // Set this to the maximum length of the rope

    private Rigidbody2D rbSelf;
    private Rigidbody2D rbConnected;

    void Start()
    {
        rbSelf = GetComponent<Rigidbody2D>();
        rbConnected = connectedPlayer.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 distanceVector = rbConnected.position - rbSelf.position;
        float currentDistance = distanceVector.magnitude;

        // Check if the current distance is greater than the maximum rope length
        if (currentDistance > maxRopeLength)
        {
            Vector2 correctionVector = distanceVector.normalized * (currentDistance - maxRopeLength);
            Vector2 force = correctionVector / Time.fixedDeltaTime;

            // Apply force to pull players back within the max rope length
            rbSelf.AddForce(force * 0.5f);
            rbConnected.AddForce(-force * 0.5f);
        }
    }
}
