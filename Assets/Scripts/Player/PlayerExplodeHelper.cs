using System.Collections.Generic;
using UnityEngine;

public class PlayerExplodeHelper : MonoBehaviour
{
    private const int AxisCubesCount = 8;

    [SerializeField]
    private GameObject player;

    public void Explode()
    {
        var cubes = new List<GameObject>();

        for (int x = 0; x < AxisCubesCount; x++)
        {
            for (int y = 0; y < AxisCubesCount; y++)
            {
                for (int z = 0; z < AxisCubesCount; z++)
                {
                    cubes.Add(CreateExplosionCube(new Vector3(x, y, z)));
                }
            }
        }

        gameObject.SetActive(false);
        Invoke(nameof(RespawnPlayer), 3f);

        foreach (var cube in cubes)
        {
            Destroy(cube, 2f);
        }
    }

    private GameObject CreateExplosionCube(Vector3 coordinates)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        cube.transform.localScale = transform.localScale / AxisCubesCount;

        var firstCubePosition = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCubePosition + Vector3.Scale(coordinates, cube.transform.localScale);
        
        var rigidBody = cube.AddComponent<Rigidbody>();
        rigidBody.AddExplosionForce(400f, transform.position, 4f);

        return cube;
    }

    public void RespawnPlayer()
    {
        Instantiate(player, new Vector3(0, 0.25f, 0), Quaternion.identity).SetActive(true);
        Destroy(gameObject);
    }
}
