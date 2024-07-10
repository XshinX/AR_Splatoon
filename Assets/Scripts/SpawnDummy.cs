using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDummy : MonoBehaviour
{
    public GameObject dummy;
    public GameObject inkling;
    public float interval = 1.0f;
    public float destroyTime = 2.0f;
    public float xmin, xmax, y, zmin, zmax;
    private float time = 0f;
    private bool GameMode;

    void Start()
	{
        GameMode = false;
	}

    void Update()
    {
        if (GameMode)
        {
            time += Time.deltaTime;
        }

        if (time > interval)
        {
            GameObject prefab = Instantiate(dummy);
            prefab.transform.position = RandomPosition();
            Destroy(prefab, destroyTime);
            time = 0f;
        }
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(xmin, xmax);
        float z = Random.Range(zmin, zmax);
        float y = inkling.transform.position.y;

        return new Vector3(x, y, z);
    }

    public void SwitchGameMode()
    {
        GameMode = !GameMode;
    }
}
