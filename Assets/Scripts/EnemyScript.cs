using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    [Header("Variables")]
    public float EnemyHealth = 999999f;
    public float time = 0f;
    public float DPS = 0f;

    [Header("Objects")]
    public TextMesh DPSDisplayer;
    public GameObject DPSDisplayerObject;

    [Header("HitboxColliders")]
    public Collider[] bodyHitboxes;
    public Collider[] headHitboxes;
    public Collider[] neckHitboxes;

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

        DPSDisplayer.text = DPS.ToString();
        
    }

    public void TakeDamage(int multiplier)
    {
        EnemyHealth -= 10 * multiplier;
    }
}
