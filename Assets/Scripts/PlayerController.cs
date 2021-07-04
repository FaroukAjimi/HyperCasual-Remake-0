using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Slider sl;
    public Text t;
    public float ms;
    public float jf;
    public float gravityScale;
    public Image fill;
    public int score = 0;
    private Vector3 md;
    public Animator anim;
    public Image win;
    public Text level;
    private int st = 0;
    public GameObject cam;
    private FixedJoystick joystick;
    private bool r, l;
    private bool check = true;
    public float rot;
    public Avatar avatar1, avatar2;
    public GameObject a1, a2;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "coin")
        {
            this.score += 1;
            other.GetComponent<Renderer>().enabled = false;
            Destroy(other);
        }
        if (other.tag == "tnt")
        {
            this.score -= 3;

            Debug.Log("Score: " + score);
            other.GetComponent<Renderer>().enabled = false;
            Destroy(other);
        }
        if (other.tag == "turn")
        {
            r = true;
        }
        if (other.tag == "turnl")
        {
            l = true;
        }
    }

void OnTriggerExit(Collider other)
    {
        if (other.tag == "finish")
        {
            win.gameObject.SetActive(true);
            level.gameObject.SetActive(false);
            anim.SetBool("walk", false);
            anim.SetBool("finish", true);
            controller.enabled = false;
            joystick.gameObject.SetActive(false);
        }
        if (other.tag == "turn")
        {
            check = true;
            r = false;
        }
        if (other.tag == "turnl")
        {
            check = true;
            l = false;
        }

    }

void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        joystick = GameObject.FindWithTag("joy").GetComponent<FixedJoystick>();
        rot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //md = new Vector3(Input.GetAxis("Horizontal") * ms, 0f, Input.GetAxis("Vertical") * ms);
        if (st == 1)
        {
            // md = (transform.forward * ms) + (transform.right * Input.GetAxis("Horizontal") * ms);
            md = (transform.forward * ms) + (transform.right * joystick.Horizontal * ms);
            md.y = md.y + (Physics.gravity.y * gravityScale);
        }
        if (Input.GetKeyDown("space") || joystick.Vertical > 0)
        {
            st = 1;
            Debug.Log("walk");
            anim.SetBool("walk", true);
        }

        controller.Move(md * Time.deltaTime);
        sl.value = this.score;
        if (score < 0)
            score = 0;
        if (score < 7)
        {
            t.color = Color.red;
            t.text = "poor";
            fill.color = Color.red;
            anim.avatar = avatar1;
            a1.SetActive(true);
            a2.SetActive(false);
        }
        if (score >= 7 && score <= 12)
        {
            t.text = "decent";
            t.color = Color.yellow;
            fill.color = Color.yellow;
            anim.avatar = avatar2;
            a1.SetActive(false);
            a2.SetActive(true);
        }
        if (score > 12)
        {
            t.color = Color.green;
            t.text = "rich";
            fill.color = Color.green;
        }
        if (score > 20)
        {
            t.color = Color.blue;
            t.text = "Millionaire";
            fill.color = Color.blue;
        }
        if (r == true)
        {
            if (check == true)
            {
                rot = rot + 90;
                check = false;
            }
            cam.GetComponent<CameraController>().RotateRight(rot);
        }
        if (l == true)
        {
            if (check == true)
            {
                rot = rot - 90;
                check = false;
            }
            cam.GetComponent<CameraController>().RotateLeft(rot);
        }
 
    }
    void FixedUpdate()
    {
        if (transform.position.y < -17)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            Time.timeScale = 1;
        }
    }
}
