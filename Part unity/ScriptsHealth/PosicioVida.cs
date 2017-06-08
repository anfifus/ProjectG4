using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PosicioVida : MonoBehaviour {
	public Camera locCamera;
	public GameObject locJugador;

	private int pixelWidth;
	private int pixelHeight;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 pos = new Vector3 (Screen.width-100, Screen.height-12, 10);//Agafa les coordenades del punts localitzat a la part dreta i dalt de la camera i les restem per poder centrar la vida

		transform.position = Camera.main.ScreenToWorldPoint (pos);//Transformala posicio del punt de la camara a la posicio del mon i llavorsli indiquem a la vida que es la posicio  que ha d'agafar
		transform.rotation = Camera.main.transform.rotation;//La rotacio de la vida ha de rotar juntament amb la camera perque no es quedi fixa

	}
}
