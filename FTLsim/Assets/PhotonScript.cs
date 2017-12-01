using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonScript : MonoBehaviour
{	public int origin_time; //Year created
	public int origin_dist; //Originates this many lightyears from Star A
	public int velocity = 1; //Direction of photon
	public int current_dist; //Lightyears from Star A
	private TimeScript time_ctrl; //Handles time events
	private Text label; //Lable stating origin_dist

	public void time_step(int time_diff) //Step forward in time
	{	current_dist += time_diff*velocity;
		transform.Translate(time_diff*velocity*10,0,0,Space.World);
		if (time_diff < 0 && time_ctrl.current_time <= origin_time)
		{	Destroy(gameObject);
		}
	}

	void Update()
	{	
	}

	public void Setup() //Can't trust Start() to be called in time
	{	time_ctrl = transform.parent.gameObject.GetComponent<TimeScript>();
		origin_time = time_ctrl.current_time;
		origin_dist = current_dist = time_ctrl.ship_dist;
		label = gameObject.GetComponentInChildren<Text>();
		label.text = origin_dist.ToString();
	}
}
