using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rs;
    public Transform pivot;
    public bool isInverted;
    private Quaternion rotation;
    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);
    }
    public void RotateRight(float rot)
    {
        Debug.Log(rot);
        target.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.Euler(target.rotation.x, rot, target.rotation.z), rs * Time.deltaTime);
        rotation = target.rotation;
    }
    public void RotateLeft(float rot)
    {
        target.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.Euler(target.rotation.x, rot, target.rotation.z), rs * Time.deltaTime);
        rotation = target.rotation;
    }
    public void RotateRoot()
    {
        target.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.Euler(0, 0, 0), rs * Time.deltaTime);
        rotation = target.rotation;
    }
}