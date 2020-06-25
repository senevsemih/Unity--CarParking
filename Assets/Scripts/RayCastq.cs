using UnityEngine;

public class RayCastq : MonoBehaviour
{
    public Transform[] objectToPlace;
    public Camera gameCamera;

    void Update()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            objectToPlace[2].position = hitInfo.point;
            objectToPlace[2].rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            if (hitInfo.collider.CompareTag("Plane"))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Instantiate(objectToPlace[0], hitInfo.point, Quaternion.identity);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Instantiate(objectToPlace[1], hitInfo.point, Quaternion.identity);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(objectToPlace[2], hitInfo.point, Quaternion.identity);
                }

                Debug.DrawRay(ray.origin, ray.direction * 50, Color.green);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
            }
        }
    }
}
