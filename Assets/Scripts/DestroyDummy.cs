using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDummy : MonoBehaviour
{
    private GameObject counter;
    //public CountPoint cp;

    void Start()
	{
        counter = GameObject.Find("PointCounter");
	}


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InkBall"))
        {
            //cp.count += 1;
            //cp.CountUp();
            //Debug.Log("Destroyed!!" + cp.count);
            counter.GetComponent<CountPoint>().CountUp();
            Destroy(this.gameObject);
        }
    }
}
