using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SO/CropDefinitionSO")]
public class CropDefinitionSO : ScriptableObject
{
    public CropType CropType;

    public TileBase SeedTile;

    public TileBase GrowingTile;

    public TileBase MatureTile;

    public float GrowTime;

    public int MinHarvestAmount;
    public int MaxHarvestAmount;

    public int HarvestAmount => Random.Range(this.MinHarvestAmount, this.MaxHarvestAmount + 1);
}
public enum CropType
{
    None,

    Rice,
    Pumpkin,// bí ngô
    Cabbage  //băp cải
}