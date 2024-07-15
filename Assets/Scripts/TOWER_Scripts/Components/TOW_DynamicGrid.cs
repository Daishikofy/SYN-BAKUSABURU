using System.Collections.Generic;
using UnityEngine;

namespace TOWER
{
    public class TOW_DynamicGrid
    {
        public Dictionary<Vector2Int, Cell> _cells;

        public TOW_DynamicGrid()
        {
            _cells = new Dictionary<Vector2Int, Cell>();
        }
    }
}