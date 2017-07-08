using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public GameObject bullet;
    public Transform gun;
    public float fireRate;
    public float rotationRate;
    public float angleRotation=10.0f;
    public float timeInPosition = 3f;
    public float angleStop1 = 90f;
    public float angleStop2 = 180f;

    private float nextFire;
    private float nextRotation;
    private float timeToMove;
    private bool rotateToRight;
    // Use this for initialization
    void Start()
    {
        timeToMove = timeInPosition;
        rotateToRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.eulerAngles.y - angleStop1) > float.Epsilon && rotateToRight)
            MakeRotation(-angleRotation);
        else if(Time.time>timeToMove && rotateToRight)
        {
            timeToMove = Time.time + timeInPosition;
            rotateToRight = false;
        }

        if (Mathf.Abs(transform.eulerAngles.y - angleStop2) > float.Epsilon && !rotateToRight)
            MakeRotation(angleRotation);
        else if (Time.time > timeToMove && !rotateToRight)
        {
            timeToMove = Time.time + timeInPosition;
            rotateToRight = true;
        }

        if (transform.eulerAngles.y == angleStop1|| transform.eulerAngles.y == angleStop2)
            FireBullet();
    }


    void FireBullet()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, gun.position, gun.rotation);
        }
    }

    void MakeRotation(float angle)
    {
        if (Time.time > nextRotation)
        {
            nextRotation = Time.time + rotationRate;
            transform.Rotate(new Vector3(0.0f, angle, 0.0f));
        }
    }
}
