using UnityEngine;
using System.Collections;

public class SplatterController : MonoBehaviour {
	
	public Material[] SplatterMaterials = new Material[0];
	
	// Use this for initialization
	void Start () {
		Material splatter = SplatterMaterials[ Random.Range( 0, SplatterMaterials.Length ) ];
		renderer.material = splatter;
		
		Vector3 localScale = transform.localScale;
		if (Random.value > 0.5f) {
			localScale.x = -1 * (localScale.x);
		} else if (rigidbody.velocity.x > 0) {
			localScale.x = 1 * (localScale.x);
		}
		transform.localScale = localScale;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
