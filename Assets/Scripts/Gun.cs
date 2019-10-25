using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // Gun Value Variables
    public float damage = 25f;
    public float range = 100f;
    public int ammoCount = 10;
    public bool allowFire = true;

    // Variables for required other instances
    public Camera fpsCam;
    public PauseMenu pm;
    public SoundManager sm;

    // Gun Polish Variables
    public Transform muzzle;
    public GameObject impactEffect;
    //public ParticleSystem muzzleFlash; muzzle flash commented out due to not actually have a PS for it :)

    // UI Elements related to the gun
    public Text ammoCountText;
    public Text reloadText;

    // Gathers required instances on Start
    void Start()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoCountText.text = ammoCount + " / 10";
        if (Input.GetKeyDown(KeyCode.Mouse0) && pm.isPaused == false)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoCount < 10)
        {
            allowFire = false;
            sm.PlaySound(sm.reload, 0.5f);
            reloadText.gameObject.SetActive(true);
            Invoke("Reload", 1.5f);
        }
    }

    // Logic for using raycasts to detect a target and deal damage to specific targets
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && ammoCount > 0 && allowFire == true)
        {
            //muzzleFlash.Play();
            sm.PlaySound(sm.gunfire, 0.5f);
            Targets target = hit.transform.GetComponent<Targets>();
            if (target != null)
                target.TakeDamage(damage);
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            allowFire = false;
            Invoke("FireDelay", 0.1f);
            ammoCount--;
        }

        else if (ammoCount <= 0)
        {
            allowFire = false;
            sm.PlaySound(sm.reload, 0.5f);
            reloadText.gameObject.SetActive(true);
            Invoke("Reload", 1.7f);
        }
    }

    // Used in invoke as a fire delay
    void FireDelay()
    {
        allowFire = true;
    }

    // Reloads the gun
    public void Reload()
    {
        ammoCount = 10;
        allowFire = true;
        reloadText.gameObject.SetActive(false);
    }
}
