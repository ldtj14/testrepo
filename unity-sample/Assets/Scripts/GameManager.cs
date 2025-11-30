using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int startingScore = 0;

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent<int> OnScoreChanged;

    public int Health { get; private set; }
    public int Score { get; private set; }

    private void Awake()
    {
        Health = startingHealth;
        Score = startingScore;
        OnHealthChanged?.Invoke(Health);
        OnScoreChanged?.Invoke(Score);
    }

    public void AddScore(int amount)
    {
        Score += Mathf.Max(0, amount);
        OnScoreChanged?.Invoke(Score);
    }

    public void TakeDamage(int amount)
    {
        Health = Mathf.Max(0, Health - Mathf.Abs(amount));
        OnHealthChanged?.Invoke(Health);

        if (Health <= 0)
        {
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game over - reload scene or show UI here.");
        Time.timeScale = 0f;
    }
}
