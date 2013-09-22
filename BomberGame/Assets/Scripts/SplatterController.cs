using UnityEngine;
using System.Collections;

public class SplatterController : MonoBehaviour {
	
	public Material[] SplatterMaterials = new Material[0];
	
	// Use this for initialization
	void Start () {
		
		// random rotation
		//transform.eulerAngles.y = Random.Range( 0, 360 );
		
		// random splatter
		Material splatter = SplatterMaterials[ Random.Range( 0, SplatterMaterials.Length ) ];
		renderer.material = splatter;
		
		// random scale
		float randomScale = (Random.value*0.2f)+0.1f;
		transform.localScale = Vector3.one * randomScale;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
