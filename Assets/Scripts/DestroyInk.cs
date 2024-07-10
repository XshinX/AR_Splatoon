using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInk : MonoBehaviour
{
    public float destroyTime;
    private int count;

    void Start()
	{
        count = 0;
	}

    void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.name == "Floor")
		{
            Destroy(this.gameObject, destroyTime);
        }
	}

    void Update()
	{
        count++;

        if (count > 200)
		{
            Destroy(this.gameObject);
        }
	}
}
