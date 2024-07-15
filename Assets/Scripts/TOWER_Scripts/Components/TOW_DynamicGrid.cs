using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TOWER
{
    public class TOW_DynamicGrid
    {
        private List<Tilemap> _obstacles;

        public TOW_DynamicGrid(List<Tilemap> obstacles)
        {
            _obstacles = obstacles;
        }

        public int GetCellValue(Vector2Int position)
        {
            foreach (Tilemap tilemap in _obstacles)
            {
                if (tilemap.GetTile((Vector3Int) position) != null)
                {
                    return Int32.MaxValue;
                }
            }
            return 0;
        }
    }
}