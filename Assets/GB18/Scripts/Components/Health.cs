using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int currentHealth;
    private bool isAlive;


    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = false;
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;

    }

    public bool CheckIsAlive()
    {
        return isAlive = currentHealth > 0;
    }

    public void Death() { 
        Destroy(gameObject);
    }
}
