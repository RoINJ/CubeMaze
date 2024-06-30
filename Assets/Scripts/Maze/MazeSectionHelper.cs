using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSectionHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    [SerializeField]
    private GameObject frontWall;

    [SerializeField]
    private GameObject backWall;

    [SerializeField]
    private GameObject filler;

    public bool IsProcessed { get; private set; }

    public void Process()
    {
        filler.SetActive(false);
        IsProcessed = true;
    }

    public void RemoveLeftWall()
    {
        leftWall.SetActive(false);
    }

    public void RemoveRightWall()
    {
        rightWall.SetActive(false);
    }

    public void RemoveFrontWall()
    {
        frontWall.SetActive(false);
    }

    public void RemoveBackWall()
    {
        backWall.SetActive(false);
    }
}
