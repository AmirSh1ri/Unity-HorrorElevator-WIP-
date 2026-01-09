using UnityEngine;


// Enemies moving towards player when not looked at !!TO BE BUILD UPON!!
public class FollowPlayerWhenNotLookedAt : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Settings")]
    public int moveSpeed;
    public float lookThreshold = 0.7f;
    public float rotationSpeed = 5f;

    [Header("Rotation Offset")]
    public Vector3 rotationOffset = Vector3.zero;
    private void Start()
    {
        moveSpeed = Random.Range(3, 6);
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }
    void Update()
    {
        if (player == null) return;
        Vector3 toThis = transform.position - player.position;
        toThis.y = 0;

        Vector3 playerForward = player.forward;
        playerForward.y = 0;
        playerForward.Normalize();

        float dot = Vector3.Dot(playerForward, toThis.normalized);

        if (dot < lookThreshold)
        {
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            Vector3 direction = (targetPos - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                targetRot *= Quaternion.Euler(rotationOffset);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
