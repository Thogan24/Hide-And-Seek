using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float EnemyHealth = 999999f;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(EnemyHealth);
    }

    public void TakeDamage()
    {
        EnemyHealth -= 10;
    }
}
