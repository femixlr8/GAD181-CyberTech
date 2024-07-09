using UnityEngine;

public class ICGridManager : MonoBehaviour
{
    public GameObject blueIconPrefab; // Assign this in the Inspector
    public GameObject redXIconPrefab; // Assign this in the Inspector
    public int rows = 5;
    public int columns = 5;
    public float spacing = 1.5f; // Adjust the spacing between icons

    private GameObject[,] grid;
    public int redIconCount { get; private set; }

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GameObject[rows, columns];
        Vector2 gridOrigin = new Vector2(-columns / 2.0f * spacing, -rows / 2.0f * spacing);

        // Place blue icons in a structured grid
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Vector2 position = gridOrigin + new Vector2(x * spacing, y * spacing);
                GameObject icon = Instantiate(blueIconPrefab, position, Quaternion.identity);
                icon.transform.SetParent(transform);
                grid[x, y] = icon;
            }
        }

        // Place red icons randomly within the grid, replacing blue icons
        PlaceRedIcons();
    }

    void PlaceRedIcons()
    {
        int numberOfRedIcons = Random.Range(7, 11); // Adjust the number of red icons here
        redIconCount = numberOfRedIcons;

        for (int i = 0; i < numberOfRedIcons; i++)
        {
            int x = Random.Range(0, rows);
            int y = Random.Range(0, columns);

            // Ensure we replace a blue icon with a red icon
            while (grid[x, y] == null || grid[x, y].tag == "RedIcon")
            {
                x = Random.Range(0, rows);
                y = Random.Range(0, columns);
            }

            // Destroy the blue icon at this position
            Destroy(grid[x, y]);

            // Calculate position based on grid origin and spacing
            Vector2 position = new Vector2(-columns / 2.0f * spacing + x * spacing, -rows / 2.0f * spacing + y * spacing);

            // Instantiate red icon
            GameObject redIcon = Instantiate(redXIconPrefab, position, Quaternion.identity);
            redIcon.transform.SetParent(transform);
            grid[x, y] = redIcon;
            redIcon.tag = "RedIcon"; // Tagging red icons for future reference if needed
        }
    }
}
