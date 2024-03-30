using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
	public string name { get; set; }
	public bool isPublic { get; set; }
	public string password { get; set; }
	public string movie { get; set; }
	public float lighting { get; set; }
	public float volume { get; set; }
	public string background { get; set; }

	public Room(string name, bool isPublic, string password, string movie, float lighting, float volume, string background)
	{
		this.name = name;
		this.isPublic = isPublic;
		this.password = password;
		this.movie = movie;
		this.lighting = lighting;
		this.volume = volume;
		this.background = background;
	}

	public Room(string name, bool isPublic, string movie, float lighting, float volume, string background)
	{
		this.name = name;
		this.isPublic = isPublic;
		this.password = "";
		this.movie = movie;
		this.lighting = lighting;
		this.volume = volume;
		this.background = background;
	}
}