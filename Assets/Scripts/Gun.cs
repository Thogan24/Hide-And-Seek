using UnityEngine;

public class Gun : MonoBehaviour
{
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

    public Camera fpsCam;

    void Update()
    {
        time += Time.deltaTime;
        if (isShooting == true){
            shootingTime += Time.deltaTime;
        }

        if (time > 0.1f)
        {
            cooldown = false;
        }

        if (time > 0.5f)
        {
            recoilCooldown = false;           
            shootingTime = 0f;
            isShooting = false;
            recoilAmount = 0f;
        }
        else
        {
            recoilAmount = shootingTime;
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
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform);
            hitRecoilPoint = hit.point + new Vector3(Random.Range(-100, 100) * 0.001f, recoilAmount + (Random.Range(-100, 100) * 0.001f), 0);
            Debug.Log(hitRecoilPoint);
            GameObject hitCircle = Instantiate(hitCircleObject, hitRecoilPoint, Quaternion.LookRotation(hit.normal));
            Destroy(hitCircle, 5f);
        }
    }
}

    
