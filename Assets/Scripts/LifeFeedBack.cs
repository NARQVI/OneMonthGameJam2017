using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFeedBack : MonoBehaviour {

	public float spawnTime;			//Tiempo de vida del objeto
	public float speed;				//Velocidad con la que se eleva
	public int lifeLost;			//Numero a mostrar
		
	public GUIStyle textStyle;		//Estilo del texto

	Transform myTransform;	 		//Transform de este componente


	void Start()
	{
		myTransform = transform;
		Destroy (gameObject,spawnTime); //Destruye el objeto luego de una cantidad de tiempo
	}

	void Update() 
	{
		Movement ();
	}

	//Llamado cada accion del usuario
	private void OnGUI()
	{
		Rect textRect = CalcRectMessage ();
		string msj = GetMessage ();
		ChangeColorStyle ();
		GUI.TextField (textRect,msj,textStyle); //Crea el text field
	}

	//Genera el movimiento hacia arriba del mensaje
	private void Movement() 
	{
		Vector3 step = Vector3.up * Time.deltaTime*speed;
		myTransform.Translate (step);
	}

	//Calcula el rectangulo del mensaje para mostrar
	private Rect CalcRectMessage()
	{
		Vector2 position = Camera.main.WorldToScreenPoint(myTransform.position);
		Rect rect = new Rect (position.x-25,Screen.height-position.y,25,30);
		return rect;
	}

	//Genera el mensaje a mostrar
	private string GetMessage()
	{
		string msj = "" + lifeLost;
		if (lifeLost > 0) //En el caso de aumentar la vida
			msj = "+" + msj;
		return msj;		
	}

	//Cambia el color del mensaje dependiendo de si pierde o gana vida
	private void ChangeColorStyle()
	{
		if (lifeLost < 0)
			textStyle.normal.textColor = Color.red;
		else
			textStyle.normal.textColor = Color.blue;
	}
}