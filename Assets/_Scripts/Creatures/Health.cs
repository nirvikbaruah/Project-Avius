using UnityEngine;

public class Health : MonoBehaviour
{

    public float StartingHealth = 10f;
    private float currentHealth;

    public void Start()
    {
        currentHealth = StartingHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
