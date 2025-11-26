using UnityEngine;
using UnityEngine.AI;

public class RoachWanderer : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = 120f;
    public float changeDirectionInterval = 1f;

    private float timer;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            float randomAngle = Random.Range(-180f, 180f);
            transform.Rotate(0, randomAngle,0);
            timer = Random.Range(0.5f, changeDirectionInterval);
        }

        if(Physics.Raycast(transform.position, transform.forward, 0.2f))
        {
            transform.Rotate(0, 180f, 0);
        }

    }
}