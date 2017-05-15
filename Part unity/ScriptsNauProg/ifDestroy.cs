using UnityEngine;
using System.Collections;

public class ifDestroy : MonoBehaviour {
	public GameObject camara;
	// Use this for initialization
	void OnDestroy()
	{
		if (camara != null) {
			camara.GetComponent<SeguidorPlayer> ().enabled = false;
		}
	}
}
