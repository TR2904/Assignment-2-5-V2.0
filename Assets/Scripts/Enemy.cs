using UnityEngine;


public class Enemy : InteractableObject

{

    public delegate void EnemyDestroyed();

    public event EnemyDestroyed OnDestroyed;


    [Header("Enemy Settings")]

    public int health = 100; // Current health

    public int maxHealth = 100; // Maximum health

    public int damage = 10; // Damage dealt by the enemy


    public override void Interact(Player player)

    {

        Debug.Log($"{gameObject.name} interacted with!");

    }


    public void TakeDamage(int damage)

    {

        health -= damage;

        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");


        if (health <= 0)

        {

            Die();

        }

    }


    private void Die()

    {

        Debug.Log($"{gameObject.name} has been destroyed!");

        OnDestroyed?.Invoke();

        Destroy(gameObject);

    }


    public void ScaleAttributes(float difficultyMultiplier)

    {

        maxHealth = Mathf.RoundToInt(maxHealth * difficultyMultiplier);

        health = maxHealth; // Reset health to scaled max

        damage = Mathf.RoundToInt(damage * difficultyMultiplier);

        Debug.Log($"{gameObject.name} scaled! Health: {maxHealth}, Damage: {damage}");

    }

}
