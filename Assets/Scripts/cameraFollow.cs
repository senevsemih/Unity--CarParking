using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public static cameraFollow Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    public List<Transform> targets = new List<Transform>();
    public Vector3 offset;
    public float smooth = 0.125f;

    void FixedUpdate()
    {
        Vector3 dPos = targets[0].position + offset;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, smooth);
        transform.position = sPos;

        transform.LookAt(targets[0].position, targets[0].rotation.eulerAngles);
    }
    public void AddTarget(Transform newTarget)
    {
        if (!targets.Contains(newTarget))
        {
            targets.Add(newTarget);
        }
    }
    public void RemoveTarget(Transform oldTarget)
    {
        if (targets.Contains(oldTarget))
        {
            targets.Remove(oldTarget);
        }
    }
}
