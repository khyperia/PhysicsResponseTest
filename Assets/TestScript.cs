using System.Text;
using UnityEditor;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Rigidbody cube;
    public Rigidbody sphere;
    private Vector3 originalPosition;
    private int currentFrame;
    private float cubeSpeed;
    private StringBuilder sb = new();
    
    private int currentTest;
    private int testCount = 300;

    private void Start()
    {
        originalPosition = cube.position;
        ResetTest();
    }

    private void ResetTest()
    {
        if (currentTest > testCount)
        {
            Debug.Log("Done!");
            EditorApplication.ExitPlaymode();
        }
        sphere.position = Vector3.zero;
        sphere.rotation = Quaternion.identity;
        sphere.velocity = Vector3.zero;
        sphere.angularVelocity = Vector3.zero;
        cube.position = originalPosition;
        cube.rotation = Quaternion.identity;
        cube.velocity = Vector3.zero;
        cube.angularVelocity = Vector3.zero;
        cubeSpeed = Mathf.Lerp(1.0f, 2.5f, (float)currentTest / testCount);
        currentTest++;
    }

    private void FixedUpdate()
    {
        if (currentFrame++ == 15)
        {
            currentFrame = 0;
            sb.AppendLine($"{cubeSpeed},{sphere.velocity.x}");
            Debug.Log(sb);
            ResetTest();
        }

        cube.MovePosition(cube.position + new Vector3(cubeSpeed * Time.fixedDeltaTime, 0, 0));
    }
}