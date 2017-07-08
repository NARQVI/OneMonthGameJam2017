using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public float life = 100f; //Models the players life
    public Text lifeText;       //Text with player life

    float moveSpeed = 4f;
    Vector3 forward, right;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.grey;
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        lifeText.text = "Life: " + life;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            Move();
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;

        transform.position += rightMovement;

        transform.position += upMovement;
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
