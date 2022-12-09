using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpParticles : MonoBehaviour
{
    public ParticleSystem doubleJumpParticles;
    public GameObject doubleJumpParticlesObject;
    public float particleTime2 = 1f;
    public GameObject player;
    void Start()
    {
        
    }
    void Update()
    {
        Debug.Log(transform.position);
        GameObject doubleJumpPrefab = Instantiate(doubleJumpParticlesObject, transform);
        doubleJumpPrefab.transform.position = player.transform.position;
        Destroy(doubleJumpPrefab, particleTime2);
    }
}
