using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public float life = 100f; //Models the players life
    public Text lifeText;       //Text with player life
    public float moveSpeed = 15f;

    Vector3 forward, right;
    Rigidbody rb;               //Thos object rigid body
    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.grey;
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        lifeText.text = "Life: " + life;

        rb = GetComponent<Rigidbody>();     //Gets rigid body component
    }

    // Update is called once per frame
    void Update()
    {
            Move();
    }

    void Move()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;

        rb.velocity += rightMovement;   //Change velocity of rigid body.
        rb.velocity += upMovement;


    }

    public void Hit(float hit)
    {
        life -= hit;
        if (life <= 0f)
        {
            lifeText.text = "Dead";
            Invoke("GameOver", 2f); // Calls GameOver function after 2seconds
        }
        else
            lifeText.text = "Life: " + life;

    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
