using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropertiesItem : MonoBehaviour {
	public Transform pathParent;
	private List<Transform> pathSon;
	private float x = 10;
	private float y = -3;
    private float alturaNau = 1.62f;
    private float z = 0;
	// Use this for initialization
	void Start () {

		getPath ();
		int numPath = Random.Range(0,pathSon.Count);//Crear un numero pseudoaleatori entre 0 i el numero total d'elements - 1
        //GetComponent<Renderer>().material.color = Color.blue;
		transform.position = new Vector3(pathSon [numPath].position.x,alturaNau, pathSon[numPath].position.z) /*+ new Vector3(x,y,z)*/;//Possiciona l'item a qualsevol punt del path
        
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(5,10,15)*Time.deltaTime);//Rota l'item sobre ell mateix

	}
    /// <summary>
    /// Permet obtenir els punts del path
    /// </summary>
	private void getPath()//Obtenir els punts del cami
	{
		var paths = pathParent.GetComponentInChildren<Transform> ();//Aconsegueix els transforms de tots els elements fill de pathParent
		pathSon = new List<Transform> ();//Crea una llista de Transform
		foreach(Transform path in paths)
		{
			pathSon.Add (path);//Afegeix cada Transform dins de la llista
		}
	}
}
