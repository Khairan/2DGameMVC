using UnityEngine;
using UnityEngine.Tilemaps;


namespace platformerMVC
{
    public class GeneratorController
    {
        private const int CountWall = 4;

        private Tilemap _tilemap;
        private Tile _groundTile;
        private int _mapWidth;
        private int _mapHeight;
        private bool _borders;

        private int _smoothFactor;
        private int _fillPercent;

        private int[,] _map;

        public GeneratorController(GeneratorLevelView levelView)
        {
            _tilemap = levelView.Tilemap;
            _groundTile = levelView.GroundTile;
            _mapWidth = levelView.MapWidth;
            _mapHeight = levelView.MapHeight;
            _borders = levelView.Borders;
            _fillPercent = levelView.FillPercent;
            _smoothFactor = levelView.SmoothFactor;

            _map = new int[_mapWidth, _mapHeight];
        }

        public void Init()
        {
            RandomFillMap();

            for (int i = 0; i < _smoothFactor; i++)
            {
                SmoothMap();
            }

            DrawTilesOnMap();
        }

        private void RandomFillMap()
        {
            var seed = Time.time.ToString();
            var pseudoRandom = new System.Random(seed.GetHashCode());

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                    {
                        if (_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = (pseudoRandom.Next(100) < _fillPercent) ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallsCount(x, y);

                    if (neighbourWallTiles > CountWall)
                        _map[x, y] = 1;
                    else if (neighbourWallTiles < CountWall)
                        _map[x, y] = 0;
                }
            }
        }

        private int GetSurroundingWallsCount(int x, int y)
        {
            int wallCount = 0;

            for (int gridX = x - 1; gridX <= x + 1; gridX++)
            {
                for (int gridY = y - 1; gridY <= y + 1; gridY++)
                {
                    if (gridX >= 0 && gridX < _mapWidth && gridY >= 0 && gridY < _mapHeight)
                    {
                        if (gridX != x || gridY != y)
                        {
                            wallCount += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private void DrawTilesOnMap()
        {
            if (_map == null)
                return;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    var positionTile = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);

                    if (_map[x, y] == 1)
                        _tilemap.SetTile(positionTile, _groundTile);
                }
            }
        }
    }
}
