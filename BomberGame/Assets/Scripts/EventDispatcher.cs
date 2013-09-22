using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler ExplosionComplete;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// OnExplosionComplete
	void OnExplosionComplete () {
	    if (ExplosionComplete != null)
		{
		        ExplosionComplete (this.gameObject);
		}
	}
}
