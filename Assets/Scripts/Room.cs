using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
	public enum Background { Theatre, Mountain, Space }
	
	public string name { get; set; }
	public int movie { get; set; }
	public Background background { get; set; }

	public Room(bool defaultVal)
	{
		this.name = "";
		this.movie = -1;
		this.background = Background.Theatre;
	}

	public Room(string name, int movie, string background)
	{
		this.name = name;
		this.movie = movie;
		this.background = Background.Theatre;
	}
}