### Project Description

The project is a simple console game written in C# where the player navigates a map represented in the console. The map is loaded from a BMP file, and different pixel colors in the image represent various types of tiles (e.g., floor, wall). The player can move around the map, rotate, and on the screen, information about the current position, viewing direction, and surroundings is displayed. The game utilizes the **SixLabors.ImageSharp** library for processing the BMP image.

### Implemented Features

- **Loading the Map from a BMP File**:
  - The map is created based on a BMP file.
  - Different pixel colors in the image are mapped to different types of tiles in the game.
  - A white pixel (RGB: 255, 255, 255) represents the player's starting position.
  - A brown pixel (RGB: 165, 42, 42) represents the floor.
  - A gray pixel (RGB: 128, 128, 128) represents a wall.
  - Additionally, other colors can be added and mapped to various types of tiles.

- **Tile Class**:
  - Represents a single tile on the map.
  - Contains properties:
    - Name (name of the tile).
    - Symbol (character representing the tile in the console).
    - IsPassable (whether the player can move through this tile).
  - Can be extended with additional effects (e.g., interactions, changes to the player's state).

- **Player Class**:
  - Represents the player in the game.
  - Properties:
    - X, Y (player's position on the map).
    - FacingDirection (direction the player is facing).
    - Symbol (character representing the player in the console, depending on the facing direction).
  - Methods:
    - Turn(Direction) (changes the facing direction and updates the symbol).
    - Move(Board) (attempts to move the player in the direction they are facing).

- **Board Class**:
  - Stores the game map as a two-dimensional array of Tile objects.
  - Responsible for loading the map from a BMP file and mapping pixels to tiles.
  - Contains a `colorTileMapping` dictionary that maps pixel colors to Tile objects.
  - Utilizes the **ImageSharp** library for image processing.

- **Player Movement Handling**:
  - The player can only move in the direction they are facing.
  - Pressing a directional key different from the current facing direction only rotates the player.
  - If the player is facing a direction and presses the corresponding key, they attempt to move to the next tile.
  - The player cannot move through tiles with `IsPassable = false` (e.g., walls).

- **Displaying Information in the Console**:
  - Information about the current tile, the tile in front of the player, and the facing direction is displayed above the map.
  - Information about the pressed key is also updated.
  - The board is displayed below the information, with appropriate characters representing tiles and the player.

- **Adjusting Map Display in the Console**:
  - Due to the proportions of characters in the console (characters are taller than they are wide), modifications were made in the code to make the map appear more square.
  - Spaces were added between characters and map lines were duplicated to compensate for the proportion differences.

- **Unicode Character Handling**:
  - The console encoding is set to UTF-8 (`Console.OutputEncoding = System.Text.Encoding.UTF8;`) to allow the display of characters outside the basic ASCII set.
  - If there are issues with displaying Unicode characters, alternative ASCII characters are used.

- **Multithreading**:
  - The game uses multithreading to handle input, game logic, and display.
  - Threads:
    - InputThread (handles user input).
    - DisplayThread (updates the console display).
    - Game (main game thread).

### File Structure and Class Division

- **Program.cs**:
  - Entry point of the program.
  - Sets the console encoding to UTF-8.
  - Disables the cursor visibility.
  - Creates an instance of the game and starts it.

- **Game.cs**:
  - Main class managing the game.
  - Properties:
    - Running (whether the game is running).
    - KeyPressed (the last pressed key).
    - Board (instance of the Board class representing the map).
    - Player (instance of the Player class representing the player).
    - Static fields PlayerStartX and PlayerStartY (initial player position on the map).
  - Method `Start()` starts the game threads.

- **Board.cs**:
  - Represents the game map.
  - Loads the map from a BMP file.
  - Maps pixel colors to Tile objects using the `colorTileMapping` dictionary.
  - Stores a two-dimensional array of Tile objects.

- **Tile.cs**:
  - Class representing a single tile on the map.
  - Properties:
    - Name (name of the tile).
    - Symbol (character representing the tile in the console).
    - IsPassable (whether it can be walked on).
  - Placeholder for additional effects (e.g., interactions).

- **Player.cs**:
  - Class representing the player.
  - Properties:
    - X, Y (position on the map).
    - FacingDirection (direction the player is facing).
    - Symbol (character representing the player in the console).
  - Methods:
    - Turn(Direction) (rotates the player).
    - Move(Board) (attempts to move the player).

- **InputThread.cs**:
  - Class responsible for handling user input.
  - Reads pressed keys and responds accordingly (rotation, movement, game termination).

- **DisplayThread.cs**:
  - Class responsible for displaying the game state in the console.
  - Updates game information and displays the map.
  - Sets the cursor position to prevent flickering and overlapping text.

- **Direction.cs** (optional, if in a separate file):
  - Enum `Direction` representing directions: Up, Down, Left, Right.

### Additional Information

- **ImageSharp Library**:
  - The project uses the **SixLabors.ImageSharp** library to load and process the BMP image.
  - It allows efficient processing of image pixels and mapping them to game objects.

- **Color Mapping to Tiles**:
  - Pixel colors in the image are mapped to tiles using the `colorTileMapping` dictionary.
  - This allows easy addition of new tile types by adding new entries to the dictionary.

- **Flexibility and Extensibility**:
  - The code structure is designed to easily add new functionalities.
  - For example, new tile types with different effects, interactions, or properties can be added.
  - The code is modular and divided into logical classes, facilitating maintenance and development.

- **Exception and Error Handling**:
  - The program includes basic exception handling, e.g., checking if the map file exists and contains the player's starting position.
  - In case of errors, the program informs the user.

- **Console Character Proportion Issues**:
  - Because console characters have different height-to-width proportions, modifications were made in the code to ensure the map displays correctly.
  - Further adjustments can be made by experimenting with adding spaces or duplicating lines.

### Possible Directions for Further Development

- **Adding Interactions with the Environment**:
  - Utilize the placeholder for additional effects in the Tile class to introduce interactions, e.g., collecting items, opening doors.

- **Expanding the Map**:
  - Add more types of tiles, such as water, fire, teleporters.
  - Implement diverse terrains with unique properties.

- **Graphical User Interface**:
  - Transition from the console to a graphical interface using libraries like **Windows Forms**, **WPF**, or **MonoGame**.
  - This would allow more advanced visual effects and better control over the display.

- **Combat or Quest System**:
  - Add enemies that the player can fight.
  - Introduce quests or missions to complete.

- **Saving and Loading Game State**:
  - Implement a mechanism to save the player's progress and load it upon restarting the game.

- **Performance Improvements**:
  - Optimize the code, e.g., by refreshing only the changed parts of the map.
  - Improve thread management.

### Summary

The project serves as a solid foundation for a simple console game with potential for further development. The code structure is clear and modular, making it easy to add new functionalities. Utilizing a BMP file to generate the map allows for easy creation and modification of game levels. By introducing the Tile class and mapping colors to tiles, the system is flexible and extensible.
