using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    public static TetrominoSpawner Instance { get; private set; }

    [SerializeField] GameObject[] Tetrominoes;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        transform.position = new Vector3(GameGrid.Instance.Width / 2, GameGrid.Instance.Height - 2, 0f);
        SpawnNext();
    }

    public void SpawnNext()
    {
        int randomIndex = Random.Range(0, Tetrominoes.Length);
        Instantiate(Tetrominoes[randomIndex], transform.position, Quaternion.identity);
    }
}
