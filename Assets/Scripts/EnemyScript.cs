using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float EnemyHealth = 999999f;
    public float time = 0f;
    public float DPS = 0f;
    public TextMesh DPSDisplayer;
    public GameObject DPSDisplayerObject;

    void Start()
    {
        DPSDisplayer = DPSDisplayerObject.GetComponent<TextMesh>() as TextMesh;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            time = 0f;
            DPS = 999999 - EnemyHealth;
            EnemyHealth = 999999f;
        }
        //Debug.Log(DPS);

        DPSDisplayer.text = DPS.ToString();
    }

    public void TakeDamage(Collider collider)
    {
        Debug.Log(collider);
        EnemyHealth -= 10;
    }
}
