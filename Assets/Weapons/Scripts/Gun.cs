using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
  public float damage = 20f;
  public float range = 10f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
      isReloading = false;
      animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
      if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
      {
        Debug.Log(hit.transform.name);

         Health health = hit.transform.GetComponent<Health>();
        if (health != null)
        {
          health.TakeDamage(damage);
        }
        GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(ImpactGO, 2f);
      }
    }

    public int GetCurrentAmmo(){
        return currentAmmo;
    }

    public int GetTotalAmmo(){
        return maxAmmo;
    }

    public int IncreaseAmmo(int ammoIncrease){
        maxAmmo += ammoIncrease;
        return maxAmmo;
    }
}
