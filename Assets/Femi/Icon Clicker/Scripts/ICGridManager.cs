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

            // Calculate position based on grid origin and spacing
            Vector2 position = new Vector2(-columns / 2.0f * spacing + x * spacing, -rows / 2.0f * spacing + y * spacing);

            // Instantiate red icon
            GameObject redIcon = Instantiate(redXIconPrefab, position, Quaternion.identity);
            redIcon.transform.SetParent(transform);
            grid[x, y] = redIcon;
        }
    }

}

