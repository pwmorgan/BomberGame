using UnityEngine;
using System.Collections;

public class SplatterController : MonoBehaviour {
	
	public Material[] SplatterMaterials = new Material[0];
	
	// Use this for initialization
	void Start () {
		Material splatter = SplatterMaterials[ Random.Range( 0, SplatterMaterials.Length ) ];
		renderer.material = splatter;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
