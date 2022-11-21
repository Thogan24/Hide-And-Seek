using UnityEngine;

public class Gun : MonoBehaviour
{
    public Player playerScript;
    public SpeedButton speedButtonScript;
    public GameObject player;
    public GameObject speedButton;
    public GameObject hitCircleObject;
    public Camera fpsCam;
    public Vector3 hitRecoilPoint;
    public float damage = 10f;
    public float range = 100f;
    public bool cooldown = true;
    public bool recoilCooldown = true;
    public float time = 0f;
    public bool isShooting = false;
    public float shootingTime = 0f;
    public float recoilAmount = 0f;
    public float movingAccuracy = 1f;
    public float jumpingAccuracy = 1f;
    public float recoilAccuracy = 1f;


    private void Start()
    {
        playerScript = player.GetComponent<Player>();
        speedButtonScript = speedButton.GetComponent<SpeedButton>();
    }

    void Update()
    {
        if (playerScript.isGrounded == false) {
            jumpingAccuracy = 20f;
        }
        else { 
            jumpingAccuracy = 1f;
        }
        time += Time.deltaTime;
        if (isShooting == true) {
            shootingTime += Time.deltaTime;
        }

        if (time > 0.1f) {
            cooldown = false;
        }

        if (time > 0.2f) {
            recoilCooldown = false;           
            shootingTime = 0f;
            isShooting = false;
            recoilAmount = 0f;
            recoilAccuracy = 1f;
        }
        else {
            if (recoilAmount < 2.5f) {
                recoilAmount = shootingTime;
            }
            else if (recoilAccuracy < 10f) {
                Debug.Log(recoilAccuracy);
                recoilAccuracy = (shootingTime - 1.5f) * 3;
            }
            recoilCooldown = true;
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

        if (Physics.Raycast(fpsCam.transform.position, new Vector3((Random.Range(-100, 100) * 0.0001f * movingAccuracy * jumpingAccuracy * recoilAccuracy) + fpsCam.transform.forward.x, (recoilAmount * 0.1f + (Random.Range(-100, 100) * 0.0001f * movingAccuracy * jumpingAccuracy * recoilAccuracy)) + fpsCam.transform.forward.y, fpsCam.transform.forward.z), out hit, range))
        {
            
            hitRecoilPoint = hit.point;
            
            
            GameObject hitCircle = Instantiate(hitCircleObject, hitRecoilPoint, Quaternion.LookRotation(hit.normal));
            Destroy(hitCircle, 5f);
            Debug.Log(hit);
            EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
            // Registers enemy as the hit transform, not the individual body parts
            if(enemy != null)
            {
                // Head Shots
                foreach (Collider h in enemy.headHitboxes)
                {
                    if (h.Equals(hit.collider))
                    {
                        Debug.Log("IT WORKED!!!! (HEAD SHOT!!!)");
                        enemy.TakeDamage(10);
                    }
                }

                // Neck Shots
                foreach (Collider n in enemy.neckHitboxes)
                {
                    if (n.Equals(hit.collider))
                    {
                        Debug.Log("IT WORKED!!!! (NECK SHOT!!!)");
                        enemy.TakeDamage(8);
                    }
                }

                // Body Shots
                foreach (Collider c in enemy.bodyHitboxes)
                {
                    if (c.Equals(hit.collider))
                    {
                        Debug.Log("IT WORKED!!!!");
                        enemy.TakeDamage(5);
                    }
                }
                
            }



            /*speedButtonScript button = hit.transform.GetComponent<speedButtonScript>();
            if(button != null)
            {
                button.ButtonPressed();
            }*/
        }
    }
}

    
