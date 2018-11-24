using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLoader : MonoBehaviour {

    [SerializeField] AudioClip loadSFX;

	// Use this for initialization
	void Start ()
    {
        AudioSource.PlayClipAtPoint(loadSFX, Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
