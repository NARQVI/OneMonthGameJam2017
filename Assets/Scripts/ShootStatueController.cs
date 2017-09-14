using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStatueController : MonoBehaviour {

	public GameObject bullet;                   //Game object that models the bullet to be fired
	public Transform gun;                       //Transform of point where the bullet will be fired
	public float fireRate;                      //Rate of fire
	public float startFireTime;					//Time in seconds to start shooting
	public float bulletVelocity;
	public int numShotsRounds;						//Rounds shooted before restarting the cycle

	private float nextFire;                     //Acumulate time for calculate next time
	float startTime;
	int numShots;
	// Use this for initialization
	void Start () {
		startTime = 0;
		nextFire = 0;
		numShots = 0;
	}
	
	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;

		if (startTime >= startFireTime)
			FireBullet ();
		if (numShots >= numShotsRounds) {
			startTime = 0;
			numShots = 0;
		}

	}

	//Used to fire a bullet
	void FireBullet()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			GameObject bulletGO=Instantiate(bullet, gun.position, gun.rotation); //Creates object bullet in game
			bulletGO.GetComponent<MoveBullet>().speed = transform.forward*bulletVelocity;
			bulletGO.transform.parent = transform;
			numShots++;
		}
	}
}
