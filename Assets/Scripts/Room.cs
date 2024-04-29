using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
	public enum Background { Theatre, Mountain, Space }
	
	public string name { get; set; }
	public string movie { get; set; }
	public Background background { get; set; }

	public Room(bool defaultVal)
	{
		this.name = "";
		this.movie = "";
		this.background = Background.Theatre;
	}

	public Room(string name, string movie, string background)
	{
		this.name = name;
		this.movie = movie;
		this.background = Background.Theatre;
	}
}