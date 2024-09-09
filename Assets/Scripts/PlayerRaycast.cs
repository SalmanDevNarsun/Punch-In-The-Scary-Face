using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
using System.Net;

public class PlayerRaycast : MonoBehaviour
{
    public float pokeForce;
    public Transform AimSprite;
    public Transform HandParent;
    public Transform ArmParent;
    public Transform HandRestPoint;
    public AudioSource GroundHitSound, EnemyHitSound;
    public ParticleSystem GroundHitEffect, HitEffect;
    public float Movespeed;
    bool aimactive;
    void Start()
    {
        
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            AimSprite.gameObject.SetActive(true);
            aimactive = true;
        }


        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine(moveHandToHitPoint(hit.point));
                
                if (hit.rigidbody != null)
                {
                    if (hit.collider.gameObject.layer == 6)
                    {
                        hit.collider.gameObject.GetComponent<EnemyDamager>()._animator.enabled = false;
                        hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);
                        EnemyHitSound.Play();
                        HitEffect.Play();
                        HapticFeedback.MediumFeedback();
                    }
                    else if (hit.collider.gameObject.layer == 7)
                    {
                        GroundHitSound.Play();
                        GroundHitEffect.Play();
                        HapticFeedback.MediumFeedback();
                    }
                    else
                    {
                        EnemyHitSound.Play();
                        HitEffect.Play();
                    }
                }
                else
                {
                    Debug.Log("No rigidbody Found");
                }
            }
            aimactive = false;
            AimSprite.gameObject.SetActive(false);
        }

        if (aimactive)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                AimSprite.position = hit.point;
                HandParent.LookAt(hit.point);
            }
        }
    }


    IEnumerator moveHandToHitPoint(Vector3 hitpoint)
    {
        //yield return new WaitForSeconds(0.1f);
        HandParent.position = Vector3.MoveTowards(HandParent.position, hitpoint, Movespeed);
        ArmParent.localScale = new Vector3(1, 1, 25);
        yield return new WaitForSeconds(0.4f);
        HandParent.position = Vector3.MoveTowards(HandParent.position, HandRestPoint.position, Movespeed);
        ArmParent.localScale = new Vector3(1, 1, 1.345f);
    }
}
