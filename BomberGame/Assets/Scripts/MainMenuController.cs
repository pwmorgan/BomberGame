using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	
	//
	public string GameName = "PUPPY BOMBS";
	public string FirstLevelName = "patrick_level1";
	public GUIStyle MainTitleStyle = new GUIStyle();
	public GUIStyle MainMenuButtonStyle = new GUIStyle();
	
	// private
	private Rect MainMenuRect;
	private float MainMenuWidth = 600.0f;
	private float MainMenuHeight = 300.0f;
	private float MainMenuSpacer = 30.0f;
	private float MainMenuButtonHeight = 25.0f;
	
	
	// Use this for initialization
	void Start () {
		
		// setup rect
		MainMenuRect = new Rect( Screen.width/2 - (MainMenuWidth/2), Screen.height/2 - (MainMenuHeight/2), MainMenuWidth, MainMenuHeight );
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//
	void OnGUI() {
		
		GUI.BeginGroup( MainMenuRect );
			GUI.Label( new Rect( 0, 0, MainMenuWidth, MainMenuButtonHeight ), GameName, MainTitleStyle );
			
		
			if (GUI.Button( new Rect( 0, (MainMenuButtonHeight + MainMenuSpacer) * 2, MainMenuWidth, 25 ), "START", MainMenuButtonStyle ) ) {
				StartGame();
			}
			if (GUI.Button( new Rect( 0, (MainMenuButtonHeight + MainMenuSpacer) * 3, MainMenuWidth, 25 ), "QUIT", MainMenuButtonStyle ) ) {
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
