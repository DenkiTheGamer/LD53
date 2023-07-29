using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnableObjects;
    private void Awake()
    {      
        Instantiate(spawnableObjects[Random.Range(0,spawnableObjects.Length)], transform.position ,Quaternion.identity);
        gameObject.SetActive(false);
    }
}
