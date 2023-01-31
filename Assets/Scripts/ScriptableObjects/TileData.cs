using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
	public TileBase[] tiles;
    // Start is called before the first frame update
   
   public int	digable, dig_strength;
}
