using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public GameObject FX_Coin;
    public AudioClip PickupSound;
    public int magnetSpeed = 10;

    private Transform _player;
    private bool follow = false;


	// Use this for initialization
	void Start () {
        _player = GameObject.Find("PlayerController").transform;
	}

    void Update()
    {
        if (LevelManager.instance.hasMagnet)
        {
            if (Vector3.Distance(gameObject.transform.position, _player.position) <= 30)
            {
                if(transform.TransformPoint(Vector3.zero).z > _player.position.z)
                    follow = true;
            }
        }

        if (follow)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                new Vector3(_player.position.x, _player.position.y + 2, _player.position.z), Time.deltaTime * magnetSpeed);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(FX_Coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
            LevelManager.instance.coinCount++;
            LevelManager.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PickupSound, 1);
        }
    }
}
