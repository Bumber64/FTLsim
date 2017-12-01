using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{	public int current_time = 0; //Current time in years
	public int ship_dist = 0; //Current distance of ship from Star A in lightyears
	public int ship_speed = 4; //Speed of ship in lightyears per year
	public GameObject ship_object; //Ship sprite
	public Text time_indicator; //Time text box
	public GameObject photon_prefab;

	void step_forward()
	{	update_photons(1);
		if (current_time <= 50)
		{	spawn_photon(true);
			spawn_photon(false);
		}
		move_ship(1);
		current_time++;
		time_indicator.text = "T = "+current_time.ToString();
	}

	void step_backward()
	{	if (current_time > 0)
		{	current_time--;
			move_ship(-1);
			update_photons(-1);
			time_indicator.text = "T = "+current_time.ToString();
		}
	}

	void spawn_photon(bool forward_vel)
	{	GameObject p = Instantiate(photon_prefab, ship_object.transform.position, Quaternion.identity, gameObject.transform);
		p.transform.Translate(0, 40+7*current_time, 0, Space.World);
		if (!forward_vel)
		{	p.GetComponent<PhotonScript>().velocity*=-1;
		}
		p.GetComponent<PhotonScript>().Setup();
	}

	void update_photons(int time_diff)
	{	PhotonScript[] photons = GetComponentsInChildren<PhotonScript>();
		foreach (PhotonScript ps in photons)
		{	ps.time_step(time_diff);
		}
	}

	void move_ship(int time_diff)
	{	if (current_time >= 10 && current_time < 15){}
		else if (current_time >= 25 && current_time < 30){}
		else if (current_time >= 50){}
		else if (current_time < 30)
		{	ship_dist += ship_speed*time_diff;
			ship_object.transform.Translate(ship_speed*time_diff*10,0,0,Space.World);
		}
		else if (current_time >= 30)
		{	ship_dist -= ship_speed*time_diff;
			ship_object.transform.Translate(-ship_speed*time_diff*10,0,0,Space.World);
		}
	}

	void Start()
	{	
	}

	void Update()
	{	if (Input.GetButtonDown("TimePlus"))
		{	step_forward();
		}
		else if (Input.GetButtonDown("TimeMinus"))
		{	step_backward();
		}
	}
}
