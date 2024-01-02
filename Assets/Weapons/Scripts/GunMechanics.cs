using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanics : MonoBehaviour
{

    // TODO :: CAN CURRENTLY ONLY SHOOT ONCE AND RELOAD AND WONT SHOOT AGAIN ATM - FIX THIS
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;
    bool Firing;

    //Reference
    public Camera fpsCam;
    public RaycastHit hit;
    //Graphics
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display
        //ammoText.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        

        //Reloading
        if (bulletsLeft <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) {
            StartCoroutine(Reload());
            return;
        }

        //shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
      private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.transform.name);

            //Hit enemy
             Health health = hit.transform.GetComponent<Health>();
        if (health != null)
        {
          health.TakeDamage(damage);
        }
        GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(ImpactGO, 2f);
        }
        //graphics
        
        muzzleFlash.Play();
        StartCoroutine(Recoil());
        
        //Shooting effects
        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);




    
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private IEnumerator Reload()
    {
        reloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private IEnumerator Recoil()
    {
        Firing = true;
        animator.SetBool("Firing", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Firing", false);
        Firing = false;
    }

    public int GetCurrentAmmo(){
        return bulletsLeft;
    }

    public int GetMagazineSize(){
        return magazineSize;
    }

    public int IncreaseAmmo(int amount){
        magazineSize += amount;
        return magazineSize;
    }
}
