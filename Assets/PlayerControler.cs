using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float acceleration = 10;
    public GameObject bulletPrefab;
    private Rigidbody rb;
    private Vector2 controlls;
    private Transform gunLeft, gunRight;
    private bool fireButtonDown = false;
    private camerascript cs;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunLeft = transform.Find("gunLeft");
        gunRight = transform.Find("gunRight");
        cs = Camera.main.GetComponent<camerascript>();
    }

    // Update is called once per frame
    void Update()
    {

        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        controlls = new Vector2(h, v);
        float maxHorizontal = cs.worldWidth / 2;
        float maxVertical = cs.worldHeight / 2;

        if (Math.Abs(transform.position.x) > maxHorizontal)
        {
            Vector3 newPosition = new Vector3(transform.position.x * -0.95f, 0, transform.position.z);
            transform.position = newPosition;
        }
        if (Math.Abs(transform.position.z) > maxVertical)
        {
            Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z * -0.95f);
            transform.position = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireButtonDown = true;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * controlls.y * acceleration, ForceMode.Acceleration);
        rb.AddTorque(transform.up * controlls.x * acceleration, ForceMode.Acceleration);

        if (fireButtonDown)
        {
            GameObject Ball1 = Instantiate(bulletPrefab, gunLeft.position, Quaternion.identity);
            Ball1.transform.parent = null;
            Ball1.GetComponent<Rigidbody>().AddForce(Ball1.transform.forward * 10, ForceMode.VelocityChange);

            Destroy(Ball1, 5);

            GameObject Ball2 = Instantiate(bulletPrefab, gunRight.position, Quaternion.identity);
            Ball2.transform.parent = null;
            Ball2.GetComponent<Rigidbody>().AddForce(Ball2.transform.forward * 10, ForceMode.VelocityChange);

            Destroy(Ball2, 5);
            fireButtonDown = false;
        }
    }
}