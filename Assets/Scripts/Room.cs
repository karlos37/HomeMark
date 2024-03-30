using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
	public string name { get; set; }
	public bool isPublic { get; set; }
	public string password { get; set; }
	public string movie { get; set; }

	public Room(string name, bool isPublic, string password, string movie)
	{
		this.name = name;
		this.isPublic = isPublic;
		this.password = password;
		this.movie = movie;
	}
}