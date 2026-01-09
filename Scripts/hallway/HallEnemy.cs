using UnityEngine;

// Hallway Enemy Moving

public class HallEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5f;

    private bool isMoving = false;

    public void Move()
    {
        isMoving = true;
    }

    public void StopMove()
    {
        isMoving = false;
    }

    void Update()
    {
        if (!isMoving || player == null) return;

        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Vector3 lookDir = targetPos - transform.position;

        // Stop With J !!OUGHT TO BE CHANGED TO CLOSE DOOR SCRIPT!!

        if (Input.GetKeyDown(KeyCode.J))
        {
            StopMove();
        }
    }
}
