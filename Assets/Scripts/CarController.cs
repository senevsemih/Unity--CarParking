using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CarController : MonoBehaviour
{
    private Rigidbody carRb;
    private Vector3 curPos, curRot;
    private float screenWidth;
    private float screenHeight;

    public GameObject powerUpPrefab;
    public bool hasPowerUp = false;
    public float carSpeed;
    public float carTurnSpeed = 5.0f;
    public bool isStun = true;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        cameraFollow.Instance.AddTarget(transform);

        curPos = this.transform.position;
        curRot = this.transform.eulerAngles;
        
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }
    void FixedUpdate()
    {
        Movement();
        Stun();
        powerUpPrefab.transform.position = transform.position;
    }
    void OnDisable()
    {
        cameraFollow.Instance.RemoveTarget(transform);
    }
    void Movement()
    {  
        Vector3 forward = (transform.forward * carSpeed);
        carRb.AddForce(forward);

        //float horizontalInput = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up * horizontalInput * carTurnSpeed);

        int i = 0;
        while(i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > screenWidth / 2 && Input.GetTouch(i).position.y < screenHeight / 2)
            {
                transform.Rotate(Vector3.up * carTurnSpeed);
            }
            if (Input.GetTouch(i).position.x < screenWidth / 2 && Input.GetTouch(i).position.y < screenHeight / 2)
            {
                transform.Rotate(Vector3.down * carTurnSpeed);
            }
            i++;
        }
    }
    void Stun()
    {
        if (isStun)
        {
            carRb.isKinematic = true;
        }
        else
        {
            carRb.isKinematic = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" && !hasPowerUp)
        {
            this.transform.position = curPos;
            this.transform.eulerAngles = curRot;
        }
        if (other.gameObject.name == "Portal_1")
        {
            Vector3 offset = new Vector3(1, -1, 0.5f);
            this.transform.position = GameObject.Find("Portal_2").transform.position + offset;
            this.transform.eulerAngles = new Vector3(0, 45, 0);
        }
        if (other.gameObject.name == "Portal_2")
        {
            Vector3 offset = new Vector3(1, -0.5f, -1);
            this.transform.position = GameObject.Find("Portal_1").transform.position + offset;
            this.transform.eulerAngles = new Vector3(0, 135, 0);
        }
        if (other.gameObject.tag == "PowerUp")
        {
            hasPowerUp = true;
            powerUpPrefab.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpRoutine());
        }
    }
    IEnumerator PowerUpRoutine()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        powerUpPrefab.SetActive(false);
    }
}
