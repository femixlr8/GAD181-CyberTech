using UnityEngine;

public class ICGridManager : MonoBehaviour
{
    public GameObject blueIconPrefab;
    public GameObject redXIconPrefab;
    public int rows = 5;
    public int columns = 5;

    private GameObject[,] grid;
    public int redIconCount { get; private set; }

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GameObject[rows, columns];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                GameObject icon = Instantiate(blueIconPrefab, new Vector2(x, y), Quaternion.identity);
                icon.transform.SetParent(transform);
                grid[x, y] = icon;
            }
        }

        PlaceRedIcons();
    }

    void PlaceRedIcons()
    {
        int numberOfRedIcons = Random.Range(1, 5);
        redIconCount = numberOfRedIcons;

        for (int i = 0; i < numberOfRedIcons; i++)
        {
            int x = Random.Range(0, rows);
            int y = Random.Range(0, columns);
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y]);
                GameObject redIcon = Instantiate(redXIconPrefab, new Vector2(x, y), Quaternion.identity);
                redIcon.transform.SetParent(transform);
                grid[x, y] = redIcon;
            }
        }
    }
}
