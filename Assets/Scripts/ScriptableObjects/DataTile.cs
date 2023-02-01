using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new DataTile", menuName ="DataTile", order = 52)]
public class DataTile : ScriptableObject
{
	public TileBase[] tiles;
	public bool digable;
	public int	strenght = 5;
}
