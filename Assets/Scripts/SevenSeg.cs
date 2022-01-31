using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class SevenSeg : MonoBehaviour
{
	// public GameObject textObj;
	public GameObject seg1;
	public GameObject seg2;
	public GameObject seg3;
	public GameObject seg4;
	public GameObject seg5;
	public GameObject seg6;
	public GameObject seg7;

	private BoxCollider _boxCollider;
	
	// public Renderer m_Renderer;

	int num = 0;
	int time = 30;
	int cycle_time = 30;
	int num_dir = -1;

	float rotate_speed = 5.0f;
	float rot_speed_min = 3.0f;
	float rot_speed_max = 60.0f;
	float rot_deg = 0.0f;

	float change_time_min = 0.2f;
	float change_time_max = 3.0f;
	float diag = 0;

	Color color;
	int dir;
	Vector3 pos;
	public Vector3 speed;
	public bool isCol;
	Vector3 otherSpeed;

	float wait;
	int thick = 90;
	int depth = 1500;
	int ceiling = -2000;

	void Awake()
	{
		_boxCollider = GetComponent<BoxCollider>();
	}

	void Start()
	{
		// m_Renderer = GetComponent<Renderer>();
		wait = 2.0f;
		color = new Color(255, 0, 0);
		dir = (int)Random.Range(0.0f, 3.0f) - 1;
		// pos = new Vector3(Screen.width / 2, Screen.height / 2, 300.0f);
		pos = new Vector3(Random.Range(-1000.0f, 1000.0f), Random.Range(-1000.0f, 1000.0f), 2000.0f);
		// pos = new Vector3(0, 0, 500.0f);
		this.transform.position = pos;
		// speed = new Vector3(Random.Range(-0.5f, 0.5f) / wait, Random.Range(-0.5f, 0.5f) / wait, Random.Range(-0.5f, 0.5f) / wait * 2);
		rotate_speed = Random.Range(rot_speed_min, rot_speed_max);
		cycle_time = (int)(Random.Range(change_time_min, change_time_max) * 24.0f);
		time = (int)(Random.Range(change_time_min, change_time_max) * 24.0f);
		float _num_dir = Random.Range(0.0f, 1.0f);
		if(_num_dir > 0.5) num_dir = 1;
		seg1.SetActive(false);seg2.SetActive(false);seg3.SetActive(false);seg4.SetActive(false);seg5.SetActive(false);seg6.SetActive(false);seg7.SetActive(false);
		// textObj.GetComponent<TextMesh>().color = Color.green;
		isCol = true;
	}

	// Update is called once per frame
	void Update()
	{
		float deltaRot = (float)dir * rotate_speed;
		rot_deg += deltaRot;
		pos = pos + speed;
		this.transform.position = pos;
		this.transform.Rotate(0, 0, deltaRot * Mathf.Deg2Rad, Space.Self);

		time--;
		if(time < 0)
		{
			num += (num_dir == 1 ? 1 : 9);
			num %= 10;
			time = cycle_time;
			switch (num)
			{
				case 0:
					seg1.SetActive(false);seg2.SetActive(false);seg3.SetActive(false);seg4.SetActive(false);seg5.SetActive(false);seg6.SetActive(false);seg7.SetActive(false);
					break;
				case 1:
					seg1.SetActive(false);seg2.SetActive(true);seg3.SetActive(true);seg4.SetActive(false);seg5.SetActive(false);seg6.SetActive(false);seg7.SetActive(false);
					break;
				case 2:
					seg1.SetActive(true);seg2.SetActive(true);seg3.SetActive(false);seg4.SetActive(true);seg5.SetActive(true);seg6.SetActive(false);seg7.SetActive(true);
					break;
				case 3:
					seg1.SetActive(true);seg2.SetActive(true);seg3.SetActive(true);seg4.SetActive(true);seg5.SetActive(false);seg6.SetActive(false);seg7.SetActive(true);
					break;
				case 4:
					seg1.SetActive(false);seg2.SetActive(true);seg3.SetActive(true);seg4.SetActive(false);seg5.SetActive(false);seg6.SetActive(true);seg7.SetActive(true);
					break;
				case 5:
					seg1.SetActive(true);seg2.SetActive(false);seg3.SetActive(true);seg4.SetActive(true);seg5.SetActive(false);seg6.SetActive(true);seg7.SetActive(true);
					break;
				case 6:
					seg1.SetActive(true);seg2.SetActive(false);seg3.SetActive(true);seg4.SetActive(true);seg5.SetActive(true);seg6.SetActive(true);seg7.SetActive(true);
					break;
				case 7:
					seg1.SetActive(true);seg2.SetActive(true);seg3.SetActive(true);seg4.SetActive(false);seg5.SetActive(false);seg6.SetActive(false);seg7.SetActive(false);
					break;
				case 8:
					seg1.SetActive(true);seg2.SetActive(true);seg3.SetActive(true);seg4.SetActive(true);seg5.SetActive(true);seg6.SetActive(true);seg7.SetActive(true);
					break;
				case 9:
					seg1.SetActive(true);seg2.SetActive(true);seg3.SetActive(true);seg4.SetActive(true);seg5.SetActive(false);seg6.SetActive(true);seg7.SetActive(true);
					break;
				default:
					break;
			}
			// textObj.GetComponent<TextMesh>().text = num.ToString();
		}

		// Vector2 temp = Camera.main.WorldToViewportPoint(pos);
		Vector2 temp = Camera.main.WorldToScreenPoint(pos);
		Vector3[] v = new Vector3[4];
		Vector2[] v_2d = new Vector2[4];
		RectTransform rectTransform = this.transform.GetComponent<RectTransform>();

		rectTransform.GetWorldCorners(v);
		bool flag = false;
		for(int i = 0; i < 4; i++)
		{
			v_2d[i] = Camera.main.WorldToScreenPoint(v[i]);
			// Debug.Log(i + ":" + v_2d[i]);
		}

		flag = false;
		for(int i = 0; i < 4; i++)
		{
			if(v_2d[i].x > (float)Screen.width) flag = true;
		}
		if(flag == true)
		{
			speed.x = -Mathf.Abs(speed.x);
			// Debug.Log("Over screen width");
			if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		}

		flag = false;
		for(int i = 0; i < 4; i++)
		{
			if(v_2d[i].x < 0.0f) flag = true;
		}
		if(flag == true)
		{
			speed.x = Mathf.Abs(speed.x);
			// Debug.Log("Over screen width");
			if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		}

		flag = false;
		for(int i = 0; i < 4; i++)
		{
			if(v_2d[i].y > (float)Screen.height) flag = true;
		}
		if(flag == true)
		{
			speed.y = -Mathf.Abs(speed.y);
			// Debug.Log("Over screen height");
			if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		}

		flag = false;
		for(int i = 0; i < 4; i++)
		{
			if(v_2d[i].y < 0.0f) flag = true;
		}
		if(flag == true)
		{
			speed.y = Mathf.Abs(speed.y);
			// Debug.Log("Over screen height");
			if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		}

		// Debug.Log(temp);

		// if(temp.x <= 120){
		// 	speed.x = Mathf.Abs(speed.x);
		// 	if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		// } 
		// else if(temp.x >= Screen.width - 120) {
		// 	speed.x = -Mathf.Abs(speed.x);
		// 	if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		// }

		// if(temp.y <= 120) {
		// 	speed.y = Mathf.Abs(speed.y);
		// 	if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		// } 
		// else if(temp.y >= Screen.height - 120) {
		// 	speed.y = -Mathf.Abs(speed.y);
		// 	if(speed.z < 0) speed.z = Mathf.Abs(speed.z);
		// }

		if(pos.z <= ceiling) speed.z = Mathf.Abs(speed.z);
		if(pos.z >= depth) speed.z = -Mathf.Abs(speed.z);
		isCol = false;
		// if(temp.x < 0.0f || temp.y < 0.0f || temp.x > 1.0f || temp.y > 1.0f || pos.z < -2500 || pos.z > 2500)
		// {

			// main.cur_cnt--;
			// Destroy(gameObject);
		// }
		// RectTransform canvas = Canvas.GetComponent<RectTransform>();

		// temp.x *= canvas.sizeDelta.x;
		// temp.y *= canvas.sizeDelta.y;
		// temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
		// temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

		// if (!m_Renderer.isVisible)
		// {
		// 	Debug.Log("Object is invisible");
		// }
	}

	private void OnCollisionEnter(Collision collision)
	{		
		GameObject other = collision.gameObject;

		if(other.GetComponent<SevenSeg>().isCol == false)
		{
			otherSpeed = other.GetComponent<SevenSeg>().speed;

			other.GetComponent<SevenSeg>().speed = speed;
			speed = otherSpeed;
		}
		isCol = true;

		// speed.z *= -1.0f;
		// other.SendMessage("SpeedChange", speed);
		// speed = otherSpeed;
		// Debug.Log("AA");
	}


	void ColorChange(Color newColor)
	{
		// textObj.GetComponent<TextMesh>().color = newColor;
		seg1.SendMessage("UpdateColor", newColor);
		seg2.SendMessage("UpdateColor", newColor);
		seg3.SendMessage("UpdateColor", newColor);
		seg4.SendMessage("UpdateColor", newColor);
		seg5.SendMessage("UpdateColor", newColor);
		seg6.SendMessage("UpdateColor", newColor);
		seg7.SendMessage("UpdateColor", newColor);
	}

	void DepthChange(int newDepth)
	{
		depth = newDepth;
	}

	void CeilingChange(int newCeiling)
	{
		ceiling = newCeiling;
	}

	void SpeedChange(Vector3 newSpeed)
	{
		speed = newSpeed;
	}

	void ThickChange(int newThick)
	{
		thick = newThick;
		_boxCollider.size = new Vector3(600, 1000, thick);
	}

}
