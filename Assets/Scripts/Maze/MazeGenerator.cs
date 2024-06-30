using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private DeadZoneHelper deadZone;

    [SerializeField]
    private MovementHelper player;

    [SerializeField]
    private MazeSectionHelper mazeSection;

    [SerializeField]
    private int width, height;

    private MazeSectionHelper[,] _mazeGrid;

    public IEnumerator GeneratLevel()
    {
        yield return GenerateMaze();

        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private IEnumerator GenerateMaze()
    {
        _mazeGrid = new MazeSectionHelper[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var section = Instantiate(mazeSection, new Vector3(x, 0, z), Quaternion.identity, transform);
                section.transform.localPosition = new Vector3(x, 0, z);
                _mazeGrid[x, z] = section;
            }
        }

        yield return GenerateMaze(null, _mazeGrid[0, 0]);
    }

    private IEnumerator GenerateMaze(MazeSectionHelper previousCell, MazeSectionHelper currentCell)
    {
        currentCell.Process();

        if (previousCell is not null)
        {
            ClearWalls(previousCell, currentCell);

            if (Random.Range(0, 100) < 10)
            {
                Instantiate(deadZone, currentCell.transform.position, Quaternion.identity, transform);
            }
        }

        yield return new WaitForSeconds(0.005f);

        MazeSectionHelper nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell is not null)
            {
                yield return GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell is not null);
    }

    private MazeSectionHelper GetNextUnvisitedCell(MazeSectionHelper currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeSectionHelper> GetUnvisitedCells(MazeSectionHelper currentCell)
    {
        int x = (int)currentCell.transform.localPosition.x;
        int z = (int)currentCell.transform.localPosition.z;

        if (x + 1 < width)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (!cellToRight.IsProcessed)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (!cellToLeft.IsProcessed)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < height)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (!cellToFront.IsProcessed)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (!cellToBack.IsProcessed)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeSectionHelper previousCell, MazeSectionHelper currentCell)
    {
        if (previousCell.transform.localPosition.x < currentCell.transform.localPosition.x)
        {
            previousCell.RemoveRightWall();
            currentCell.RemoveLeftWall();
        }
        else if (previousCell.transform.localPosition.x > currentCell.transform.localPosition.x)
        {
            previousCell.RemoveLeftWall();
            currentCell.RemoveRightWall();
        }
        else if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z)
        {
            previousCell.RemoveFrontWall();
            currentCell.RemoveBackWall();
        }
        else if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z)
        {
            previousCell.RemoveBackWall();
            currentCell.RemoveFrontWall();
        }
    }
}