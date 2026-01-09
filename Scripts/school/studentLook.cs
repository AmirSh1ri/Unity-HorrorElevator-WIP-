using UnityEngine;

// Rotation of the students (AND ALL THE NPCS) heads towards the player
public class RotateTowardsPlayer : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;
    public float detectionRange = 5f;
    public Vector3 rotationOffset;// Offset

    void Update()
    {
        if (player == null) return;

        // Get distance to player
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            // Find direction to player
            Vector3 direction = (player.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Apply rotation offset
            Quaternion offsetRotation = Quaternion.Euler(rotationOffset);
            lookRotation *= offsetRotation;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }
}
