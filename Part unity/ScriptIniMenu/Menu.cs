using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        //Permet canviar d'escena concretament a Menu del usuari on s'inicia l'inici de sessió de l'usuari
        SceneManager.LoadScene("MenuUsuari");
    }

    public void Exit()
    {
        //Permet sortir de l'aplicacio
        Application.Quit();
    }

}
