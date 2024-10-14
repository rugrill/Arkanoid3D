using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject prefab;


    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the child at index i
            Transform childTransform = transform.GetChild(i);

            // Spawn the prefab at the child's position and rotation
            Instantiate(prefab, childTransform.position, childTransform.rotation);
        }
    }

    void Update()
    {

    }

}
