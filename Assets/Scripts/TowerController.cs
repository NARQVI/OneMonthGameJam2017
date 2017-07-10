using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Class that models the behaviour of the tower 
 */

public class TowerController : MonoBehaviour {

    public GameObject bullet;                   //Game object that models the bullet to be fired
    public Transform gun;                       //Transform of point where the bullet will be fired
    public float fireRate;                      //Rate of fire
    public float rotationRate;                  //Rotation rate
    public float angleRotation=10.0f;           //Angle of rotation in a repetition
    public float timeInPosition = 3f;           //Time that the tower waits before moving
    public float angleStop1 = 90f;              //Stop Angles
    public float angleStop2 = 180f;             //Stop Angles
        
    private float nextFire;                     //Acumulate time for calculate next time
    private float nextRotation;                 //Acumulate time for calculate next rotation
    private float timeToMove;                   //Acumulate time for calculate wait time in position
    private bool rotateToRight;                 //Boolean checks the rotation diretion

    // Use this for initialization
    void Start()
    {
        timeToMove = timeInPosition;
        rotateToRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.eulerAngles.y - angleStop1) > float.Epsilon && rotateToRight) //Checks if current angle is bigger than angleStop1 
            MakeRotation(-angleRotation);
        else if(Time.time>timeToMove && rotateToRight)  //Active when limit is reached
        {
            timeToMove = Time.time + timeInPosition;
            rotateToRight = false;  //Change direction of rotation
        }

        if (Mathf.Abs(transform.eulerAngles.y - angleStop2) > float.Epsilon && !rotateToRight) //Checks if current angle is bigger than angleStop2
            MakeRotation(angleRotation);
        else if (Time.time > timeToMove && !rotateToRight)
        {
            timeToMove = Time.time + timeInPosition;
            rotateToRight = true;
        }

        if (transform.eulerAngles.y == angleStop1|| transform.eulerAngles.y == angleStop2)
            FireBullet();
    }

    //Used to fire a bullet
    void FireBullet()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, gun.position, gun.rotation); //Creates object bullet in game
        }
    }

    //Used to make a single rotation
    void MakeRotation(float angle)
    {
        if (Time.time > nextRotation)
        {
            nextRotation = Time.time + rotationRate;
            transform.Rotate(new Vector3(0.0f, angle, 0.0f)); // set the rotation given by the angles in x,y,z
        }
    }
}
