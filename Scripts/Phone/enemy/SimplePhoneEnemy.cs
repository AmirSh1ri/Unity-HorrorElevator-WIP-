using UnityEngine;
// UNUSED CODE FOR ENEMY IN PHONE ROOM
public class SimplePhoneEnemy : MonoBehaviour
{
    public Transform player;
    public float teleportDistance = 3f;
    public Vector3 rotationOffset;

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            MoveCloser();
        }
    }

    void MoveCloser()
    {
        if (player == null) return;
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * teleportDistance;
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);
    }
}
