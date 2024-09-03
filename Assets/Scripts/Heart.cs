using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public GameObject FX_Heart;
    public AudioClip PickupSound;
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(FX_Heart, transform.position, Quaternion.identity);
            Destroy(gameObject);
            LevelManager.instance.heartCount++;
            LevelManager.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PickupSound, 1);

        }
    }
	
}
