using UnityEngine;

// rotation for trees for VHS MINIGAME 1
public class TreeRotation : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;
    public float detectionRange = 5f;
    public Vector3 rotationOffset;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
                Quaternion offsetRotation = Quaternion.Euler(rotationOffset);
                lookRotation *= offsetRotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
