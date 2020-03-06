using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    enum FireType {
        SINGLE,
        BURST,
        FULLAUTO
    }
    // Gun Value Variables
    public float damage = 25f;
    public float range = 100f;
    public int ammoCount = 10;
    public bool allowFire = true;
    [SerializeField] private FireType fireType;
    [SerializeField] private float timeBetweenBurst;
    [SerializeField] private float timeBetweenFullAuto;
    [SerializeField] private float numBurstShots;

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
            if (fireType == FireType.SINGLE)
            {
                Shoot();
            } else if (fireType == FireType.BURST)
            {
                StartCoroutine("BurstFire");
            } else if (fireType == FireType.FULLAUTO)
            {
                StartCoroutine("FullAutoFire");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoCount < 10)
        {
            allowFire = false;
            sm.PlaySound(sm.reload, 0.5f);
            reloadText.gameObject.SetActive(true);
            Invoke("Reload", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(fireType == FireType.BURST)
            {
                fireType = FireType.FULLAUTO;
            }
            else if (fireType == FireType.FULLAUTO)
            {
                fireType = FireType.BURST;
            }
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
            Recoil();
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

    IEnumerator BurstFire()
    {
        for(int i = 0; i < numBurstShots && ammoCount > 0; ++i)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenBurst);
        }
        if (ammoCount <= 0)
        {
            Shoot(); //Getting the Reload Logic going if they run out of ammo
        }
    }

    IEnumerator FullAutoFire()
    {
        while(Input.GetKey(KeyCode.Mouse0) && ammoCount > 0)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenFullAuto);
        }
        if (ammoCount <= 0)
        {
            Shoot(); //Getting the Reload Logic going if they run out of ammo
        }
    }

    void Recoil()
    {
        Debug.Log("RECOIL HERE");
    }
}
