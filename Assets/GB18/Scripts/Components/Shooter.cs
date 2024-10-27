using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject prop;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform shootPoint;


    public void Shoot(float direction) {
        GameObject propObject = Instantiate(prop, shootPoint.position, Quaternion.Euler(0, 0, 90 * direction));

        propObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
    }
}
