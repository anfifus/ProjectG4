using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForma : MonoBehaviour {
    public RectTransform pos;//Permet conseguir els del Canvas a que fa referencia
    
	// Use this for initialization
	void Start () {
	}
	void OnGUI()
	{
        /* float x =  630;
         float y =  230;*/
        float x = (Screen.width+pos.rect.x)-150;//Conte la posicio x
        float y = (Screen.height + pos.rect.y)-120;//Conte la posicio y
        float altura = (Screen.height / pos.rect.height) + 250;//Conte l'altura
        float amplada = (Screen.width / pos.rect.width) + 350;//Conte l'amplada
        GUI.Box(new Rect(x,y,amplada,altura),"Login");//Creacio d'una GUI.Box  on contindra diversos elements
	}
}
