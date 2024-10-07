using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool isDestroyable;
    [SerializeField]
    private float hitDelay = 1;

    private Dictionary<string, Coroutine> coroutines;

    private void Awake()
    {
        coroutines = new Dictionary<string, Coroutine>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damagable"))
        {
            collision.gameObject.GetComponent<CreatureController>().TakeDamage(damage);
        }

        if (isDestroyable)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("In -  " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Damagable"))
        {
            coroutines.Add(collision.gameObject.name, StartCoroutine(StartCollision(collision, damage)));
        }

        if (isDestroyable)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Out - " + collision.gameObject.name);
        StopCoroutine(coroutines[collision.gameObject.name]);
        coroutines.Remove(collision.gameObject.name);
    }

    public IEnumerator StartCollision(Collision2D collision, int damage) {
        while (true)
        {
            collision.gameObject.GetComponent<CreatureController>().TakeDamage(damage);
            yield return new WaitForSeconds(hitDelay);
        }
    }

}
