using System.Collections;
using UnityEngine;

// Subway Enemy Logic
public class enemySubway : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private ParticleSystem death;
    [SerializeField] private enemySubway attackCode;
    [SerializeField] private GameObject obj;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Increase movement speed when close
        float speed = distance < 10f ? moveSpeed * 3f : moveSpeed;

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player);
    }

    public void destroy()
    {
        StartCoroutine(DEATH());
    }

    // Death Logic
    private IEnumerator DEATH()
    {
        death.Play();
        attackCode.enabled = false;
        obj.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
