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

        // List to store NPC positions detected during map loading
        public List<(int X, int Y)> NPCPositions { get; private set; }

        Rgba32 yellow = new Rgba32();

        public Board(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Map file not found.", imagePath);

            InitializeColorTileMapping();

            // Initialize NPCPositions before loading the map
            NPCPositions = new List<(int X, int Y)>();

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
            yellow = new Rgba32(255, 255, 0); // Color representing NPC position

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

            // Note: Do not map the yellow color to a tile; it's used for NPC positions
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
                            else if (AreColorsEqual(pixel, yellow)) // Compare using a method
                            {
                                // Store NPC position for later initialization
                                NPCPositions.Add((x, y));
                                // Set the underlying tile as default
                                Grid[y, x] = defaultTile;
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

        // Helper method to compare colors
        private bool AreColorsEqual(Rgba32 color1, Rgba32 color2)
        {
            return color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
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
