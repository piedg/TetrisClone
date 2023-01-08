using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public static GameGrid Instance { get; private set; }

    [SerializeField] GameObject EmptyBlock;
    [SerializeField] AudioClip DeletedRowSFX;

    [field: SerializeField] public int Width { get; private set; } = 10;
    [field: SerializeField] public int Height { get; private set; } = 20;
    [field: SerializeField] public Transform[,] Grid { get; private set; }

    [SerializeField] float CamOffsetX;
    [SerializeField] float CamOffsetY;

    Camera cam;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        cam = Camera.main;
    }

    private void Start()
    {
        Grid = new Transform[Width, Height];

        CreateVisualGrid();
        CenterCameraToGrid();
    }

    public void UpdateGrid(Transform tetromino)
    {
        foreach (Transform block in tetromino)
        {
            int xPosInt = Mathf.RoundToInt(block.position.x);
            int yPosInt = Mathf.RoundToInt(block.position.y);

            Grid[xPosInt, yPosInt] = block;
        }

        CheckRow();
        TetrominoSpawner.Instance.SpawnNext();
    }

    void CheckRow()
    {
        for (int h = Height - 1; h >= 0; h--)
        {
            if (IsFullRow(h))
            {
                DeleteRow(h);
                MoveRowDown(h);
            }
        }
    }

    bool IsFullRow(int row)
    {
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            if (Grid[x, row] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteRow(int row)
    {
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            Destroy(Grid[x, row].gameObject);
            Grid[x, row] = null;
            AudioManager.Instance.PlayClip(DeletedRowSFX);
            GameManager.Instance.AddPoints(100);
        }
    }

    void MoveRowDown(int row)
    {
        for (int y = row; y < Grid.GetLength(1); y++)
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                if (Grid[x, y] != null)
                {
                    Grid[x, y - 1] = Grid[x, y];
                    Grid[x, y] = null;
                    Grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }

    void CreateVisualGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var emptyBlock = Instantiate(EmptyBlock, new Vector2(x, y), Quaternion.identity);
                emptyBlock.transform.SetParent(transform);
                emptyBlock.name = $"Block {x} {y}"; 
            }
        }
    }

    void CenterCameraToGrid()
    {
        cam.transform.position = new Vector3(((float)Width / 2) + CamOffsetX, ((float)Height / 2) + CamOffsetY, -10f);
    }
}
