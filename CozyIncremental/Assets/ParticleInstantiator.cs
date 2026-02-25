using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstantiator : MonoBehaviour
{
    public GameObject parts;
    public bool spawnAtMouse = true;

    public void SpawnObject()
    {
        Vector3 spawnPos = transform.position;
        if (spawnAtMouse)
        {
            spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPos.z = 0;
        }
        Instantiate(parts).transform.position = spawnPos;
    }
}
