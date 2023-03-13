using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidscript : MonoBehaviour
{
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;


        Vector3 playerVector = player.position - transform.position;
        transform.GetComponent<Rigidbody>().AddForce(playerVector.normalized * speed, ForceMode.VelocityChange);

        Vector3 randomVector = new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
        transform.GetComponent<Rigidbody>().AddTorque(randomVector);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Bullet"))
        {

            Destroy(other);

            Destroy(gameObject);
        }
    }
}
