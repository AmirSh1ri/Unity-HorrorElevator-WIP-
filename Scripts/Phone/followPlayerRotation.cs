using UnityEngine;
// Code For The Rotation of the UI element of the dialogues
public class followPlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform player;

    void LateUpdate()
    {
        if (player == null) return;

        transform.LookAt(player);

        transform.Rotate(0, 180f, 0);
    }
}
