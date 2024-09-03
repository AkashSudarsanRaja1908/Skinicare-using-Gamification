using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
    private Transform _player;
    private Animator _animator;
    private bool hasAppeared=false;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("PlayerController").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, _player.position) < 150 && !hasAppeared)
        {
            _animator.SetTrigger("Appear");
            hasAppeared = true;
        }
	}
}
