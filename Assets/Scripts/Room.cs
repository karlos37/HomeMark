using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
	public enum Background { Theatre, Mountain, Space }
	
	public string name { get; set; }
	public bool isPublic { get; set; }
	public string password { get; set; }
	public string movie { get; set; }
	public float lighting { get; set; }
	public float volume { get; set; }
	public Background background { get; set; }

	public Room(bool defaultVal)
	{
		this.name = "";
		this.isPublic = false;
		this.password = "";
		this.movie = "";
		this.lighting = 50f;
		this.volume = 50f;
		this.background = Background.Theatre;
	}

	public Room(string name, bool isPublic, string password, string movie, float lighting, float volume, string background)
	{
		this.name = name;
		this.isPublic = isPublic;
		this.password = password;
		this.movie = movie;
		this.lighting = lighting;
		this.volume = volume;
		this.background = Background.Theatre;
	}

	public Room(string name, bool isPublic, string movie, float lighting, float volume, string background)
	{
		this.name = name;
		this.isPublic = isPublic;
		this.password = "";
		this.movie = movie;
		this.lighting = lighting;
		this.volume = volume;
		this.background = Background.Theatre;
	}
}