using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExplodeBomb : MonoBehaviour
{
    public float destroyTime;
    public GameObject explosion;
    public GameObject ink;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            Destroy(this.gameObject, destroyTime);
            Invoke("InstantiateParticle", destroyTime - 0.1f);
            Invoke("ExplodeInk", destroyTime - 0.1f);

            //StartCoroutine(DelayMethod(destroyTime - 0.1f, () =>
            //{
            //    Debug.Log("Delay call");
            //    InstantiateInk(0);
            //}));

        }
    }

    void InstantiateParticle()
	{
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }

    void InstantiateInk(int i, float pieces, float scale)
    {
        float theta = 360.0f / pieces;
        float sin = Mathf.Sin(theta * i * Mathf.PI / 180.0f) * scale;
        float cos = Mathf.Cos(theta * i * Mathf.PI / 180.0f) * scale;

        GameObject ball = Instantiate(ink, this.transform.position + this.transform.up / 5.0f + this.transform.right * cos + this.transform.forward * sin, Quaternion.identity);

        //Rigidbody rid = ball.GetComponent<Rigidbody>();
        //rid.AddForce(Quaternion.AngleAxis(60 * i, Vector3.up) * this.transform.forward * rid.mass * 0.1f, ForceMode.Impulse);
    }

    void ExplodeInk()
	{
        int i;
        int pieces = 7;
        float pieces_float = (float)pieces;

        for (i = 0; i < pieces; i++)
        {
            InstantiateInk(i, pieces_float, 0.05f);
        }
    }

    //private IEnumerator DelayMethod(float waitTime, Action action)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    action();
    //}
}
