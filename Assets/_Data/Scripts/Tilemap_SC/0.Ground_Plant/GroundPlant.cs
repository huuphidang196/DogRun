using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundPlant : MonoBehaviour
{
    [SerializeField] private Tilemap m_Tilemap;

    private const int PlotSize = 3;

    public virtual void Plant(Vector3Int origin, CropDefinitionSO crop, int growthStage)
    {
        if (crop == null)
            return;

        TileBase tiles = GetStageTiles(crop, growthStage);

        if (tiles == null)
            return;

        for (int y = 0; y < PlotSize; y++)
        {
            for (int x = 0; x < PlotSize; x++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                m_Tilemap.SetTile(cell, tiles);
            }
        }
    }

    private TileBase GetStageTiles(CropDefinitionSO crop, int growthStage)
    {
        return growthStage switch
        {
            0 => crop.SeedTile,
            1 => crop.GrowingTile,
            2 => crop.MatureTile,
            _ => null
        };
    }

    public virtual void ClearPlot(Vector3Int origin)
    {
        for (int y = 0; y < PlotSize; y++)
        {
            for (int x = 0; x < PlotSize; x++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                m_Tilemap.SetTile(cell, null);
            }
        }
    }

    public virtual void SetPlotAlpha(Vector3Int origin, float alpha)
    {
        Color color = new Color(1f, 1f, 1f, alpha);

        for (int y = 0; y < PlotSize; y++)
        {
            for (int x = 0; x < PlotSize; x++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                m_Tilemap.SetColor(cell, color);
            }
        }
    }
}
