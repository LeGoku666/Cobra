using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Cobra
{
    public class Board
    {
        public Tile[,] Grid { get; set; }

        public int Width => Grid.GetLength(1);   // Kolumny (X)
        public int Height => Grid.GetLength(0);  // Wiersze (Y)

        private Dictionary<Rgba32, Tile> colorTileMapping;
        private Tile defaultTile;

        public Board(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Plik mapy nie został znaleziony.", imagePath);

            InitializeColorTileMapping();
            LoadMapFromImage(imagePath);
        }

        private void InitializeColorTileMapping()
        {
            colorTileMapping = new Dictionary<Rgba32, Tile>();

            // Definiujemy kolory
            var brown = new Rgba32(165, 42, 42);
            var green = new Rgba32(0, 255, 0);
            var white = new Rgba32(255, 255, 255);
            var gray = new Rgba32(128, 128, 128);

            // Tworzymy kafelki
            defaultTile = new Tile("Podłoga", '°', true); // Domyślny kafelek
            var grassTile = new Tile("Trawa", ',', true);
            var startTile = new Tile("Start gracza", ' ', true); // Znak będzie nadpisany przez gracza
            var wallTile = new Tile("Ściana", '■', false);

            // Mapujemy kolory na obiekty Tile
            colorTileMapping[brown] = defaultTile;
            colorTileMapping[green] = grassTile;
            colorTileMapping[white] = startTile;
            colorTileMapping[gray] = wallTile;

            // Możesz dodać więcej mapowań tutaj
        }

        private void LoadMapFromImage(string imagePath)
        {
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = image.Width;
                int height = image.Height;

                Grid = new Tile[height, width];

                bool playerFound = false;

                image.ProcessPixelRows(accessor =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        Span<Rgba32> pixelRow = accessor.GetRowSpan(y);
                        for (int x = 0; x < width; x++)
                        {
                            Rgba32 pixel = pixelRow[x];

                            if (colorTileMapping.TryGetValue(pixel, out Tile tile))
                            {
                                if (tile.Name == "Start gracza")
                                {
                                    if (!playerFound)
                                    {
                                        // Ustawienie początkowej pozycji gracza
                                        Game.PlayerStartX = x;
                                        Game.PlayerStartY = y;
                                        playerFound = true;
                                    }
                                    // Zamieniamy na domyślny kafelek po ustawieniu gracza
                                    Grid[y, x] = defaultTile;
                                }
                                else
                                {
                                    Grid[y, x] = tile;
                                }
                            }
                            else
                            {
                                // Używamy domyślnego kafelka
                                Grid[y, x] = defaultTile;
                            }
                        }
                    }
                });

                if (!playerFound)
                {
                    throw new Exception("Mapa nie zawiera białego piksela reprezentującego startową pozycję gracza.");
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return Grid[y, x];
            else
                return null;
        }
    }
}
