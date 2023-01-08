using UnityEngine;

public class Tetromino : MonoBehaviour
{
    [SerializeField] float DefaultTickRate = 0.85f;
    [SerializeField] float FastTickRate = 0.10f;
    [SerializeField] float DefaultHorizontalTickRate = 0.20f;
    [SerializeField] Vector3 RotationPivot;
    [SerializeField] AudioClip BlockPlacedSFX;

    private float tickRate;
    private float horizontalTickRate;

    private float nextTick;
    private float nextHorizontalTick;

    private void Start()
    {
        tickRate = DefaultTickRate;
        horizontalTickRate = DefaultHorizontalTickRate;

        if (!IsValidMove())
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameInPause()) return;

        HandleInputs();
        MoveDown();
    }

    void HandleInputs()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Time.time > nextHorizontalTick)
            {
                nextHorizontalTick = Time.time + horizontalTickRate;
                transform.position += Vector3.left;

                if (!IsValidMove())
                {
                    transform.position += Vector3.right;
                }
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Time.time > nextHorizontalTick)
            {
                nextHorizontalTick = Time.time + horizontalTickRate;
                transform.position += Vector3.right;

                if (!IsValidMove())
                {
                    transform.position += Vector3.left;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(RotationPivot), Vector3.forward, 90);

            if (!IsValidMove())
            {
                transform.RotateAround(transform.TransformPoint(RotationPivot), Vector3.forward, -90);
            }
        }
    }

    void MoveDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextTick = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            tickRate = FastTickRate;
            GameManager.Instance.AddPoints(1);
        }
        else
        {
            tickRate = DefaultTickRate;
        }

        if (Time.time > nextTick)
        {
            nextTick = Time.time + tickRate;
            transform.position += Vector3.down;
            if (!IsValidMove())
            {
                AudioManager.Instance.PlayClip(BlockPlacedSFX);
                transform.position += Vector3.up;
                GameGrid.Instance.UpdateGrid(transform);
                this.enabled = false;
            }
        }
    }

    bool IsValidMove()
    {
        foreach (Transform block in transform)
        {
            int xPosInt = Mathf.RoundToInt(block.position.x);
            int yPosInt = Mathf.RoundToInt(block.position.y);

            if (xPosInt < 0 ||
                xPosInt >= GameGrid.Instance.Width ||
                yPosInt < 0)
            {
                return false;
            }

            if (GameGrid.Instance.Grid[xPosInt, yPosInt] != null)
            {
                return false;
            }
        }
        return true;
    }
}
