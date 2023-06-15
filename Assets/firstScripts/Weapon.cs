using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float distance = 100f;
    public float pushForce = 10f;
    public int clipSize = 20;
    public float reloadTime = 2f;
    public Camera cam;
    public ParticleSystem muzzleFlash;

    public GameObject hitEffect;
    public int remainingBullets = 20;
    private bool ableToShoot;

    void Start() {
        ableToShoot = true;
    }
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (ableToShoot) {
                Shoot();
            } else {
                StartCoroutine("Reload");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && remainingBullets < clipSize) {
            StartCoroutine("Reload");
        } 
    }
    void Shoot() {
        muzzleFlash.Play();

        remainingBullets--;
        if (remainingBullets == 0) {
            ableToShoot = false;
        }

        RaycastHit hit;
        
        if ( Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            Debug.Log (hit.transform.name);

            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            GameObject targetMuzzle = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            Destroy(targetMuzzle,2f);

            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * pushForce );
            }


        }
        Debug.Log (hit.distance);
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(reloadTime);
        ableToShoot = true;
        remainingBullets = clipSize;
    }
}
