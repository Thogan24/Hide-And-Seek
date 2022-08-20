using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float EnemyHealth = 999999f;
    public float time = 0f;
    public float DPS = 0f;

    void Start()
    {
        
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
        Debug.Log(DPS);
    }

    public void TakeDamage()
    {
        EnemyHealth -= 10;
    }
}
