using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f; // damage of bullet impacts
    public float range = 100f; // range bullets will travel
    public float fireRate = 15f; // fire rate of weapon

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam; // reference to player camera
    public ParticleSystem muzzleFlash; // muzzleFlash particle effect to use
    public GameObject impactEffect; // impact effect to use
    public Animator animator;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameisPaused == false)
        {
            if (isReloading)
                return;

            if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo))
            {
                StartCoroutine(Reload());
                return; // stops from going to next if statement
            }

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) // is the player holding the fire1 button
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(); // shoot bullet
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    /// <summary>
    /// Handles shooting mechanics
    /// </summary>
    void Shoot ()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit; // store hit object information

        // Physics.Raycast sends a ray out from where fpscam is facing based on transform position, places information of hit target into hit, will only hit objects 100 away, returns true or false based on if something is hit or not
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) // if we hit an object, store target 
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>(); // finds target component of hit target and stores into target, if no target hit target = null

            if (enemy != null) // if we hit target, call TakeDamage(float amount) from target class to damage object
            {
                enemy.TakeDamage(damage);
            }

            // instantiate (place into the world) impact effect using impactEffect at hit.point (point of impact), pointing at us (using Quaternion with direction hit.normal)
            // then store into gameobject variable so we can destroy it
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); 
            Destroy(impactGO, 2f); // destroy impact effects after 2 seconds
        }
    }

    public int getCurrentAmmo()
    {
        return currentAmmo;
    }
}
