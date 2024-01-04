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

    public int maxAmmo;
    private int currentAmmo;

    //bools
    bool shooting, readyToShoot, reloading;
    bool Firing;

    public bool inSelection;

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
        currentAmmo = maxAmmo - magazineSize;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display
        //ammoText.SetText(bulletsLeft + " / " + magazineSize);
        inSelectionScreen();
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        //Reloading
        if (bulletsLeft <= 0 && currentAmmo >= 1 && !reloading)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && currentAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
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
        reloading = false;
        if (currentAmmo >= magazineSize)
        {
            currentAmmo -= magazineSize - bulletsLeft;
            bulletsLeft = magazineSize;
        }
        else if (currentAmmo < magazineSize && currentAmmo + bulletsLeft >= magazineSize)
        {
            currentAmmo -= magazineSize - bulletsLeft;
            bulletsLeft = magazineSize;
        }
        else if (currentAmmo < magazineSize )
        {
            bulletsLeft += currentAmmo;
            currentAmmo = 0;
        }
        Debug.Log("Reloaded");
    }

    private IEnumerator Recoil()
    {
        Firing = true;
        animator.SetBool("Firing", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Firing", false);
        Firing = false;
    }

    private void inSelectionScreen()
    {
        if (inSelection == true)
        {
            animator.SetBool("inSelection", true);
        }
        else
        {
            animator.SetBool("inSelection", false);
        }
    }

    public int GetCurrentAmmo()
    {
        return bulletsLeft;
    }
    public int GetCurrentAmmoCount()
    {
        return currentAmmo;
    }

    public int GetMagazineSize()
    {
        return magazineSize;
    }

    public void IncreaseAmmo(int amount)
    {
        maxAmmo += amount;
        currentAmmo += amount;
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
    }

    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }

}
