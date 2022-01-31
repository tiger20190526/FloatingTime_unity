using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]
public class Parameters
{
	public string name;
	public string value;
}

[System.Serializable]
public class configParams
{
	public Parameters[] parameters;
}


public class main : MonoBehaviour
{

	public GameObject myPrefab;
	public static int cur_cnt;

	bool invalid = true;
	int num, idx;
	int time = 30;
	int osw = 300;
	float wait = -1;
	int depth = 1500;
	int ceiling = -2000;


	Color[] color;
	Color back_color = Color.black;
	Vector3[] pos, speed;
	int[] timing, rot, dir;
	float cycle_time_min = 1.0f;
	float cycle_time_max = 5.0f;
	float cycle_time = 24.0f;
	int thick = 10;

	string configureFile = System.IO.Path.GetFullPath("config.json");

	// Start is called before the first frame update
	void Start()
	{
		Cursor.visible = false;
		string config = File.ReadAllText(configureFile, Encoding.UTF8);
		configParams root = JsonUtility.FromJson<configParams>(config);
		num = 15;
		cur_cnt = 0;
		idx = 0;
		// float wait = 20.0f;
		color = new Color[num];
		timing = new int[num];
		rot = new int[num];
		pos = new Vector3[num];
		dir = new int[num];
		speed = new Vector3[num];

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "NumOfCounter")
			{
				num = int.Parse(root.parameters[i].value);
				color = new Color[num];
				timing = new int[num];
				rot = new int[num];
				pos = new Vector3[num];
				dir = new int[num];
				speed = new Vector3[num];
				break;
			}
		}

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "Depth")
			{
				depth = int.Parse(root.parameters[i].value);
				break;
			}
		}

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "Ceiling")
			{
				ceiling = int.Parse(root.parameters[i].value);
				break;
			}
		}

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "Background")
			{
				string color_val = root.parameters[i].value;
				int color_int = 0;
				color_int = int.Parse(color_val, System.Globalization.NumberStyles.HexNumber);
				back_color = XYZ2RGB(color_int);
				break;
			}
		}

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "MovementWait")
			{
				wait = int.Parse(root.parameters[i].value);
				break;
			}
		}

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "Thickness")
			{
				thick = int.Parse(root.parameters[i].value);
				break;
			}
		}

		for(int i = 0; i < root.parameters.Length; i++)
		{
			if(root.parameters[i].name == "OSWait")
			{
				osw = int.Parse(root.parameters[i].value);
				break;
			}
		}

        //Debug.Log(osw);

		if(wait == -1)
		{
			wait = 30 + (50 - num) / 2;
			wait *= osw;
		}
        //Debug.Log(wait);

		for(int i = 0; i < num; i++)
		{
			string param_str = "Color" + i.ToString();
			Color font_color = Color.black;
			color[i] = font_color;
			pos[i] = new Vector3(Random.Range(-1000.0f, 1000.0f), Random.Range(-1000.0f, 1000.0f), Random.Range(300.0f, 2000.0f));
			speed[i] = new Vector3(Random.Range(-1000.0f, 1000.0f) / wait, Random.Range(-1000.0f, 1000.0f) / wait, Random.Range(-1000.0f, 1000.0f) / wait * 2);
			// speed[i] = new Vector3(0, 0, Random.Range(-500.0f, 500.0f) / wait * 2);

			int j;

			for(j = 0; j < root.parameters.Length; j++)
			{
				if(root.parameters[j].name == param_str) break;
			}

			if(j < root.parameters.Length)
			{
				string color_val = root.parameters[j].value;
				int color_int = 0;
				color_int = int.Parse(color_val, System.Globalization.NumberStyles.HexNumber);
				font_color = XYZ2RGB(color_int);
				color[i] = font_color;
			}

			param_str = "Speed" + i.ToString();
			for(j = 0; j < root.parameters.Length; j++)
			{
				if(root.parameters[j].name == param_str) break;
			}
			timing[i] = (int)(Random.Range(0, 3000) + 100.0f);
			if(j < root.parameters.Length)
			{
				int.TryParse(root.parameters[j].value, out timing[i]);
			}
			
			param_str = "Direction" + i.ToString();
			for(j = 0; j < root.parameters.Length; j++)
			{
				if(root.parameters[j].name == param_str) break;
			}

			dir[i] = -1;
			if(j < root.parameters.Length)
			{
				string dir_val = root.parameters[j].value;
				if(dir_val == "down")
				{
					dir[i] = 0;
				}
				else
				{
					dir[i] = 1;
				}
			}
		}

		// Camera cam;
		// cam = GetComponent<Camera>();
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = back_color;
	}

	// Update is called once per frame
	void Update()
	{
		if (invalid == true)
		{
			time--;
			if (time <= 0 && cur_cnt < num)
			{
				float spawn_cycle_time = Random.Range(cycle_time_min, cycle_time_max) * cycle_time;
				time = (int)spawn_cycle_time;
				// time = timing[idx % num];
				//time = 500;
				GameObject newObject = Instantiate(myPrefab, Vector2.zero, Quaternion.identity);
				newObject.SendMessage("ColorChange", color[idx % num]);
				newObject.SendMessage("SpeedChange", speed[idx % num]);
				newObject.SendMessage("ThickChange", thick);
				newObject.SendMessage("DepthChange", depth);
				newObject.SendMessage("CeilingChange", ceiling);
				cur_cnt++;
				idx++;
			}
		}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			Application.Quit();
        }
	}

	Color XYZ2RGB(int xyzVal){
		float fx = (float)((xyzVal & 0xFF0000) >> 16);
		float fy = (float)((xyzVal & 0xFF00) >> 8);
		float fz = (float)((xyzVal & 0xFF));
		fx /= 255.0f;
		fy /= 255.0f;
		fz /= 255.0f;
		// // Debug.Log(fx + ":" + fy + ":" + fz);
		// float D65x = 0.9505f;
		// float D65y = 1.0f;
		// float D65z = 1.0890f;
		// float delta = 6.0f/29.0f;
		// float x = (fx > delta)? D65x * (fx*fx*fx) : (fx - 16.0f/116.0f)*3f*(delta*delta)*D65x;
		// float y = (fy > delta)? D65y * (fy*fy*fy) : (fy - 16.0f/116.0f)*3f*(delta*delta)*D65y;
		// float z = (fz > delta)? D65z * (fz*fz*fz) : (fz - 16.0f/116.0f)*3f*(delta*delta)*D65z;
		// float r = x*3.2410f - y*1.5374f - z*0.4986f;
		// float g = -x*0.9692f + y*1.8760f - z*0.0416f;
		// float b = x*0.0556f - y*0.2040f + z*1.0570f;
		// r = (r<=0.0031308f)? 12.92f*r : (1f+0.055f)* Mathf.Pow(r, (1.0f/2.4f)) - 0.055f;
		// g = (g<=0.0031308f)? 12.92f*g : (1f+0.055f)* Mathf.Pow(g, (1.0f/2.4f)) - 0.055f;
		// b = (b<=0.0031308f)? 12.92f*b : (1f+0.055f)* Mathf.Pow(b, (1.0f/2.4f)) - 0.055f;
		// r = (r<0)? 0 : r;
		// g = (g<0)? 0 : g;
		// b = (b<0)? 0 : b;
		// return new Color(r, g, b);
		return new Color(fx, fy, fz);
	}
}
