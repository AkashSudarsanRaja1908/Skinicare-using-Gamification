using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {


    public GameObject FX_Magnet;
    public AudioClip PickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(FX_Magnet, transform.position, Quaternion.identity);
            LevelManager.instance.Magnet();
            Destroy(gameObject);
            LevelManager.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PickupSound, 1);
        }
    }
}
