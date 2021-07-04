using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float ms;
    private Vector3 md;
    public CharacterController controller;
    public bool check;
    public Transform ch;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "yellow")
        {
            check = true;
        }
        if (other.tag == "back")
        {
            check = false;
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == true)
        {
            md = (transform.forward * ms);
        }
        if (check == false)
        {
            md = -(transform.forward * ms);           
        }
        controller.Move(md * Time.deltaTime);
    }
}
