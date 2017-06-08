using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public Button btnEventPlay;
	public Button btnEventOption;
	public GameObject menu;
	public GameObject opcio;
	// Use this for initialization
	void Start () {
		btnEventPlay.onClick.AddListener (MenuUsuari);
		btnEventOption.onClick.AddListener (MenuOpcio);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MenuUsuari()
    {
        //Permet canviar d'escena concretament a Menu del usuari on s'inicia l'inici de sessió de l'usuari
        SceneManager.LoadScene("MenuUsuari");
    }
	public void MenuOpcio()
	{
		menu.SetActive(false);//Desactiva el menu actual
		opcio.SetActive(true);//Activa el menu on hi ha les opcions
	}
    public void Exit()
    {
        //Permet sortir de l'aplicacio
        Application.Quit();
    }

}
