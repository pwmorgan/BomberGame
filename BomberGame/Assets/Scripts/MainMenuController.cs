using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	
	//
	public string GameName = "PUPPY BOMBS";
	public string FirstLevelName = "patrick_level1";
	public GUIStyle MainMenuStartStyle = new GUIStyle();
	public GUIStyle MainMenuQuitStyle = new GUIStyle();
	
	// private
	private Rect MainMenuRect;
	private float MainMenuWidth = 600.0f;
	private float MainMenuHeight = 300.0f;
	private float MainMenuSpacer = 30.0f;
	private float MainMenuButtonHeight = 63.0f;
	
	
	// Use this for initialization
	void Start () {
		
		// setup rect
		MainMenuRect = new Rect( Screen.width/2 + 50, Screen.height/2 + 50, MainMenuWidth, MainMenuHeight );
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//
	void OnGUI() {
		
		GUI.BeginGroup( MainMenuRect );

			if (GUI.Button( new Rect( 0, 0, 263, 59 ), "", MainMenuStartStyle ) ) {
				StartGame();
			}
			if (GUI.Button( new Rect( 0, 59, 263, 59 ), "", MainMenuQuitStyle ) ) {
				QuitGame();
			}
		GUI.EndGroup();
		
	}
	
	void StartGame() {
		Application.LoadLevel( FirstLevelName );	
	}
	
	void QuitGame() {
		Application.Quit();	
	}
}
