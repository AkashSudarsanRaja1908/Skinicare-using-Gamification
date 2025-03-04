var uvAnimationTileX = 24; //Here you can place the number of columns of your sheet. 
                           //The above sheet has 24
 
var uvAnimationTileY = 1; //Here you can place the number of rows of your sheet. 
                          //The above sheet has 1
var framesPerSecond = 10.0;
var c=0;
var x=0;
function Update () {
   
    if(c==0)
    {
        x=Time.time;
        c=1;
      //  var cc : int = (Time.time) * framesPerSecond;
    }
	// Calculate index
    var index : int = (Time.time-x) * framesPerSecond;
    if(index==0)
        index=1;
    //Destroy I3sar 
    if(index>=uvAnimationTileX*uvAnimationTileY)
        Destroy(gameObject);

	// repeat when exhausting all frames
	index = index % (uvAnimationTileX * uvAnimationTileY);
 
	// Size of every tile
	var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);
 
	// split into horizontal and vertical index
	var uIndex = index % uvAnimationTileX;
	var vIndex = index / uvAnimationTileX;
 
	// build offset
	// v coordinate is the bottom of the image in opengl so we need to invert.
	var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);
 
	GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	GetComponent.<Renderer>().material.SetTextureScale ("_MainTex", size);
}