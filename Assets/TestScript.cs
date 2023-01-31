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
    public int testCount = 100;
    public int resetAfterFrames = 15;

    private void Start()
    {
        Debug.Log($"Will take {testCount * resetAfterFrames * Time.fixedDeltaTime} seconds");
        originalPosition = cube.position;
        ResetTest();
    }

    private void ResetTest()
    {
        if (currentTest != 0)
            sb.AppendLine($"{cubeSpeed},{sphere.velocity.x}");
        if (currentTest > testCount)
        {
            Debug.Log(sb);
            Debug.Log("Done! Copy the above log as CSV.");
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
        if (currentFrame++ == resetAfterFrames)
        {
            currentFrame = 0;
            ResetTest();
            if (!cube.isKinematic)
                cube.AddForce(new Vector3(cubeSpeed, 0, 0), ForceMode.VelocityChange);
        }

        if (cube.isKinematic)
            cube.MovePosition(cube.position + new Vector3(cubeSpeed * Time.fixedDeltaTime, 0, 0));
    }
}