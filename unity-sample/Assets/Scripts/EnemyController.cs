using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int damage = 1;

    private int currentIndex;

    private void Update()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            return;
        }

        Transform target = patrolPoints[currentIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager != null && other.CompareTag("Player"))
        {
            manager.TakeDamage(damage);
        }
    }
}
