using UnityEngine;

public class Gun : MonoBehaviour
{
    public Player playerScript;
    public GameObject player;
    public float damage = 10f;
    public float range = 100f;
    public GameObject hitCircleObject;
    public bool cooldown = true;
    public bool recoilCooldown = true;
    public float time = 0f;
    public bool isShooting = false;
    public float shootingTime = 0f;
    public float recoilAmount = 0f;
    public Vector3 hitRecoilPoint;
    public float movingAccuracy = 1f;
    public float jumpingAccuracy = 1f;
    public float recoilAccuracy = 1f;

    public Camera fpsCam;

    private void Start()
    {
        playerScript = player.GetComponent<Player>();
    }
    void Update()
    {
        if (playerScript.isGrounded == false)
        {
            jumpingAccuracy = 20f;
        }
        else
        {
            jumpingAccuracy = 1f;
        }
        time += Time.deltaTime;
        if (isShooting == true){
            shootingTime += Time.deltaTime;
        }

        if (time > 0.1f)
        {
            cooldown = false;
        }

        if (time > 0.2f)
        {
            recoilCooldown = false;           
            shootingTime = 0f;
            isShooting = false;
            recoilAmount = 0f;
            recoilAccuracy = 1f;
        }
        else
        {
            if (recoilAmount < 2.5f)
            {
                recoilAmount = shootingTime;
            }
            else if (recoilAccuracy < 10f)
            {
                Debug.Log(recoilAccuracy);
                recoilAccuracy = (shootingTime - 1.5f) * 3;
            }
            recoilCooldown = true;
            //Debug.Log(recoilAmount);
        }

        if (Input.GetMouseButton(0) && cooldown == false)
        {
            isShooting = true;
            cooldown = true;
            time = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (jumpingAccuracy > 1f && movingAccuracy > 1f)
        {
            jumpingAccuracy = 10f;
            movingAccuracy = 5f;
        }

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform);
            hitRecoilPoint = hit.point + new Vector3(Random.Range(-100, 100) * 0.001f * movingAccuracy * jumpingAccuracy * recoilAccuracy, recoilAmount + (Random.Range(-100, 100) * 0.001f * movingAccuracy * jumpingAccuracy * recoilAccuracy), 0);
            //Debug.Log(hitRecoilPoint);
            
            GameObject hitCircle = Instantiate(hitCircleObject, hitRecoilPoint, Quaternion.LookRotation(hit.normal));
            Destroy(hitCircle, 5f);

            EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
            if(enemy != null)
            {
                enemy.TakeDamage();
            }
        }
    }
}

    
