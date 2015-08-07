using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour {

	private Text buttonText;

	public void LoadLevelNew(){
		//Load first level
		Application.LoadLevel ("GameLevel1");
	}

	public void LoadSaveLevel(){
		Application.LoadLevel ("Credits");

	}

	public void LoadOption(){


	}

	public void QuitGame(){
		//Quit the game
		Application.Quit ();
	}

	public void Back(){
		Application.LoadLevel ("MainMenu");
	}

	public void ButtonHover(Text buttonText){
		//Set the button colour to yellow on hover
		buttonText.color = Color.yellow;
	}

	public void ButtonHoverOff(Text buttonText){
		//Set the text color back to white when the mouse goes off it
		buttonText.color = Color.white;
	}
}
