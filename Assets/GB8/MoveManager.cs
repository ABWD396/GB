using UnityEngine;

public class MoveManager : MonoBehaviour
{
    Transform objectTransform;
    public Transform[] path;

    public bool isRunning = true;

    public float speed = 5;

    private byte nextPoint = 0;

    private byte pointCount;

    private bool isLerpMovement = false;

    void Start()
    {
        objectTransform = GetComponent<Transform>();
        pointCount = (byte)path.Length;

        objectTransform.LookAt(path[nextPoint]);
        isLerpMovement = Random.value > 0.5f;
    }

    void Update()
    {
        objectTransform.Rotate(0, 0, 180 * Time.deltaTime);

        if (!isRunning)
        {
            return;
        }

        if (Vector3.Distance(objectTransform.position, path[nextPoint].position) <= 0.5f)
        {
            nextPoint++;
            if (nextPoint > pointCount - 1)
            {
                nextPoint = 0;
            }

            isLerpMovement = Random.value > 0.5f;

            float savedZ = objectTransform.rotation.eulerAngles.z;
            objectTransform.LookAt(path[nextPoint]);
            Quaternion savedRotation = objectTransform.rotation;
            objectTransform.rotation = Quaternion.Euler(savedRotation.eulerAngles.x, savedRotation.eulerAngles.y, savedZ);
        }

        if (isLerpMovement)
        {
            objectTransform.position = Vector3.Lerp(objectTransform.position, path[nextPoint].position, speed * Time.deltaTime);
        }
        else
        {
            objectTransform.position = Vector3.MoveTowards(objectTransform.position, path[nextPoint].position, speed * Time.deltaTime);
        }
    }
}
