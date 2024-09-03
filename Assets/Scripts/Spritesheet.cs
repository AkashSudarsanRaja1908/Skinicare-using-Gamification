using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spritesheet : MonoBehaviour {
    public int X = 10;
    public int Y = 10;
    public int framesPerSecond = 10;
    public int cc = 0;
    public float x = 0;
	
	// Update is called once per frame
	void Update () {
		 if(cc==0)
    {
        x=Time.time;
        cc=1;
      //  int cc  =(int) Time.time * framesPerSecond;
    }
	// Calculate index
    int index = (int) (Time.time-x) * framesPerSecond;
    if(index==0)
        index=1;
    //Destroy I3sar 
    if(index>=X*Y)
        Destroy(gameObject);

	// repeat when exhausting all frames
	index = index % (X * Y);
 
	// Size of every tile
	Vector2 size = new Vector2(1.0f / X, 1.0f / Y);
 
	// split into horizontal and vertical index
	float uIndex = index % X;
	float vIndex = index / X;
 
	// build offset
	// v coordinate is the bottom of the image in opengl so we need to invert.
	Vector2 offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);
 
	GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	GetComponent<Renderer>().material.SetTextureScale ("_MainTex", size);
	}
}
