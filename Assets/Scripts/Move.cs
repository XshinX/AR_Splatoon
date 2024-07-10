using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent	<Animator>();
    }
    void Update()
    {
        animator.SetFloat("a_Run", -1);
        if (Input.GetKeyDown ("joystick button 0")) {
            Debug.Log ("button0");
        }
        if (Input.GetKeyDown ("joystick button 1")) {
            Debug.Log ("button1");
        }
        if (Input.GetKeyDown ("joystick button 2")) {
            Debug.Log ("button2");
        }
        if (Input.GetKeyDown ("joystick button 3")) {
            Debug.Log ("button3");
        }
        if (Input.GetKeyDown ("joystick button 4")) {
            Debug.Log ("button4");
        }
        if (Input.GetKeyDown ("joystick button 5")) {
            Debug.Log ("button5");
        }
        if (Input.GetKeyDown ("joystick button 6")) {
            Debug.Log ("button6");
        }
        if (Input.GetKeyDown ("joystick button 7")) {
            Debug.Log ("button7");
        }
        if (Input.GetKeyDown ("joystick button 8")) {
            Debug.Log ("button8");
        }
        if (Input.GetKeyDown ("joystick button 9")) {
            Debug.Log ("button9");
        }
        //if (Input.GetKey("joystick button 3"))
        if (Input.GetKey("up"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position += transform.forward * speed;
            animator.SetFloat("a_Run", 1);
            Debug.Log("button3");
        }
 
        //if(Input.GetKey("joystick button 0"))
        if (Input.GetKey("down"))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position += transform.forward * speed;
            animator.SetFloat("a_Run", 1);
            Debug.Log("button0");
        }
 
 
        //if(Input.GetKey("joystick button 1"))
        if (Input.GetKey("right"))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position += transform.forward * speed;
            animator.SetFloat("a_Run", 1);
            Debug.Log("button1");
        }
 
        //if(Input.GetKey("joystick button 2"))
        if (Input.GetKey("left"))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            transform.position += transform.forward * speed;
            animator.SetFloat("a_Run", 1);
            Debug.Log("button2");
        }
    }

}
