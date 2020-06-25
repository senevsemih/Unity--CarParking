using UnityEngine;

public class ParkControler : MonoBehaviour
{
    private Light lt;
    public GameObject parkPrefab;
    public bool hasPark = false;

    void Update()
    {
        lt = GameObject.Find("Spot Light").GetComponent<Light>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            other.GetComponent<CarController>().enabled = false;
            lt.color = Color.green;
            hasPark = true;
            Destroy(parkPrefab, 1);
        }
    }
}
