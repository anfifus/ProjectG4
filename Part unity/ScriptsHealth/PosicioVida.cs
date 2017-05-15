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

		/*float camHalfHeight = Camera.main.orthographicSize;
		float camHalfWidth = Camera.main.aspect * camHalfHeight;*/

		/*Vector3 posicio = locJugador.transform.position;

		posicio += (Camera.main.transform.up * camHalfHeight) + (Camera.main.transform.right * camHalfWidth);
		transform.position = posicio;*/

		Vector3 pos = new Vector3 (Screen.width-100, Screen.height-12, 10);//Agafa les coordenades del punts localitzat a la part dreta i dalt de la camera i les restem per poder centrar la vida

		transform.position = Camera.main.ScreenToWorldPoint (pos);//Transformala posicio del punt de la camara a la posicio del mon i llavorsli indiquem a la vida que es la posicio  que ha d'agafar
		transform.rotation = Camera.main.transform.rotation;//La rotacio de la vida ha de rotar juntament amb la camera perque no es quedi fixa

		/*Vector3 posicio = locCamera.transform.position;
		posicio += (locCamera.transform.forward * 5)+ (-locCamera.transform.up * 5);
		transform.position = posicio + new Vector3 (0, 10, 0);*/



		//Bounds bounds = GetComponent<CanvasRenderer> ().bounds;

		//Vector3 topRightPosition = new Vector3(camHalfWidth,camHalfHeight,locJugador.transform.position.z)+Camera.main.transform.position;

		//topRightPosition += new Vector3 (bounds.size.x / 2, bounds.size.y / 2, 0);
		//transform.position = topRightPosition;

		//this.transform.position = locPlayer.transform.position + new Vector3(0,10,0);
		/*Vector3 position = locJugador.transform.position;
		position += (locJugador.transform.up  )+(locJugador.transform.right);
		transform.position = position + new Vector3(0,4,0);  */
		/*transform.position = locJugador.transform.position;
		transform.position += new Vector3 (locCamera.pixelWidth/40, locCamera.pixelHeight/40, locJugador.transform.position.z);*/
		//transform.position += (locJugador.transform.up / locCamera.pixelHeight )+(locJugador.transform.right / locCamera.pixelWidth)/*+new Vector3(0,4,0);*/
		//Vector3 v3 = new Vector3(Camera.main.pixelWidth,Camera.main.pixelHeight,locJugador.transform.position.z);

		//transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - locJugador.transform.right.x,Screen.height - locJugador.transform.up.y,Camera.main.farClipPlane));
		/*var pos = new Vector3(locJugador.transform.position.x,locJugador.transform.position.y,locJugador.transform.position.z);
		transform.position = locCamera.ViewportToWorldPoint (pos);*/
	}
}
