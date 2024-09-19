using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Cobra
{
    public class Board
    {
        public Tile[,] Grid { get; set; }

        public int Width => Grid.GetLength(1);   // Columns (X)
        public int Height => Grid.GetLength(0);  // Rows (Y)

        private Dictionary<Rgba32, Tile> colorTileMapping;
        private Tile defaultTile;

        public Board(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Map file not found.", imagePath);

            InitializeColorTileMapping();
            LoadMapFromImage(imagePath);
        }

        private void InitializeColorTileMapping()
        {
            colorTileMapping = new Dictionary<Rgba32, Tile>();

            // Define colors from BMP
            var brown = new Rgba32(165, 42, 42);
            var green = new Rgba32(0, 255, 0);
            var white = new Rgba32(255, 255, 255);
            var gray = new Rgba32(128, 128, 128);

            // Create tiles with assigned console colors
            defaultTile = new Tile("Floor", '°', true, ConsoleColor.DarkYellow); // Brown
            var grassTile = new Tile("Grass", ',', true, ConsoleColor.Green);
            var startTile = new Tile("Player Start", ' ', true, ConsoleColor.White); // White
            var wallTile = new Tile("Wall", '■', false, ConsoleColor.Gray);

            // Map BMP colors to Tile objects
            colorTileMapping[brown] = defaultTile;
            colorTileMapping[green] = grassTile;
            colorTileMapping[white] = startTile;
            colorTileMapping[gray] = wallTile;

            // Add more mappings here if needed
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
                                if (tile.Name == "Player Start")
                                {
                                    if (!playerFound)
                                    {
                                        // Setting the player's starting position
                                        Game.PlayerStartX = x;
                                        Game.PlayerStartY = y;
                                        playerFound = true;
                                    }
                                    // Replace with default tile after setting player
                                    Grid[y, x] = defaultTile;
                                }
                                else
                                {
                                    Grid[y, x] = tile;
                                }
                            }
                            else
                            {
                                // Use the default tile
                                Grid[y, x] = defaultTile;
                            }
                        }
                    }
                });

                if (!playerFound)
                {
                    throw new Exception("The map does not contain a white pixel representing the player's starting position.");
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
