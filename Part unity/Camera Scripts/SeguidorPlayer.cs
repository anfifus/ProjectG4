using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[ExecuteInEditMode]
public class SeguidorPlayer : MonoBehaviour {

	public GameObject Jugador;

	// Use this for initialization
	float distanceX = 0f;
	[SerializeField]private float distanceY = 10f;
	float distanceZ = -0f;


	void Update()
	{
		
		Vector3 camPosition = Jugador.transform.position;//Aconsegueix la posicio del jugador
		camPosition += -Jugador.transform.right * 10;//La modifica de manera que quedi una mica allunyada enrere
		transform.position =  camPosition + new Vector3(distanceX,distanceY,distanceZ);//Li indiquem la posicio

		transform.LookAt (Jugador.transform);//La camera observa el jugador
		
	}


}
