// Niko Väänänen

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    private float fps = 60f;

    Camera MainCamera;
    private GameObject player;
    private int moveTimer;
    private float cameraY, cameraX;
    private float xMin, xMax, xDiff, yMin, yMax, yDiff;
    private float xMaxWorld, xMinWorld, yMinWorld, yMaxWorld;
    Vector3 viewportPosition, viewportCenter, cameraWorldMax, cameraWorldMin, cameraWorldCenter, playerWorldPosition, cameraMove, cameraBoundsCheck;
    private bool yBounds, yBoundsLower, yBoundsHigher, xBounds, xBoundsLower, xBoundsHigher;

    private void MoveCamera()
    {
        cameraWorldMax = MainCamera.ViewportToWorldPoint(new Vector3(0.7f, 0.7f, 10.00f));
        cameraWorldMin = MainCamera.ViewportToWorldPoint(new Vector3(0.3f, 0.3f, 10.00f));
        cameraWorldCenter = MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10.00f));
        playerWorldPosition = player.transform.position;
        if (xBounds == false)
        {
            if (xBoundsHigher == true)
            {
                xDiff = cameraWorldMax.x - playerWorldPosition.x;
                if (xDiff == 0)
                {
                    xDiff = -0.1f;
                }
            }

            else if (xBoundsLower == true)
            {
                xDiff = cameraWorldMin.x - playerWorldPosition.x;
                if (xDiff == 0)
                {
                    xDiff = 0.1f;
                }
            }
        }
        else if (xBounds == true && yBounds == true && moveTimer >= 20)
        {
            xDiff = cameraWorldCenter.x - playerWorldPosition.x;
            xDiff = Mathf.Clamp(xDiff, -0.1f, 0.1f);
        }

        if (yBounds == false)
        {
            if (yBoundsHigher == true)
            {
                yDiff = cameraWorldMax.y - playerWorldPosition.y;
                if (yDiff == 0)
                {
                    yDiff = -0.1f;
                }
            }
            else if (yBoundsLower == true)
            {
                yDiff = cameraWorldMin.y - playerWorldPosition.y;
                if (yDiff == 0)
                {
                    yDiff = 0.1f;
                }
            }
        }
        else if (yBounds == true && xBounds == true && moveTimer >= 20)
        {
            yDiff = cameraWorldCenter.y - playerWorldPosition.y;
            yDiff = Mathf.Clamp(yDiff, -0.1f, 0.1f);
        }

        cameraMove = new Vector3(xDiff, yDiff, 0f);
        xDiff = 0f;
        yDiff = 0f;
        if (cameraMove != Vector3.zero)
        {
            cameraBoundsCheck = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            cameraBoundsCheck = cameraBoundsCheck - cameraMove;
            if (cameraBoundsCheck.x < xMinWorld)
            {
                cameraMove.x = 0f;
            }
            if (cameraBoundsCheck.x > xMaxWorld)
            {
                cameraMove.x = 0f;
            }
            if (cameraBoundsCheck.y < yMinWorld)
            {
                cameraMove.y = 0f;
            }
            if (cameraBoundsCheck.y > yMaxWorld)
            {
                cameraMove.y = 0f;
            }
            cameraBoundsCheck = Vector3.zero;
            MainCamera.transform.position = MainCamera.transform.position - cameraMove;
        }
        cameraMove = Vector3.zero;
    }

    void CheckPosition()
    {
        viewportPosition = MainCamera.WorldToViewportPoint(player.transform.position);
        if (viewportPosition.x > xMax)
        {
            xBounds = false;
            xBoundsHigher = true;
            xBoundsLower = false;
            moveTimer = 20;
        }
        else if (viewportPosition.x < xMin)
        {
            xBounds = false;
            xBoundsLower = true;
            xBoundsHigher = false;
            moveTimer = 20;
        }
        else
        {
            xBounds = true;
            xBoundsLower = false;
            xBoundsHigher = false;
        }
        if (viewportPosition.y > yMax)
        {
            yBounds = false;
            yBoundsHigher = true;
            yBoundsLower = false;
            moveTimer = 20;
        }
        else if (viewportPosition.y < yMin)
        {
            yBounds = false;
            yBoundsLower = true;
            yBoundsHigher = false;
            moveTimer = 20;
        }
        else
        {
            yBounds = true;
            yBoundsLower = false;
            yBoundsHigher = false;
        }
        if (yBounds == true && xBounds == true)
        {
            if (viewportPosition == viewportCenter)
            {
                moveTimer = 0;
                return;
            }
            else
            {
                moveTimer = moveTimer + 1;
            }

        }
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        MainCamera = Camera.main;
        player = GameObject.Find("PlayerObject");
        viewportCenter = new Vector3(0.50f, 0.50f, 10.00f);
        xMaxWorld = 20;
        xMinWorld = -20;
        yMaxWorld = 10;
        yMinWorld = -10;
        moveTimer = 0;
        xMin = 0.3f;
        xMax = 0.7f;
        yMin = 0.3f;
        yMax = 0.7f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        viewportPosition = MainCamera.WorldToViewportPoint(player.transform.position);
        CheckPosition();
        MoveCamera();
    }

    void OnGUI()
    {
        float newFPS = 1.0f / Time.unscaledDeltaTime;
        fps = Mathf.Lerp(fps, newFPS, 0.0005f);
        GUI.Label(new Rect(0, 0, 100, 100), "FPS: " + ((int)fps).ToString());
    }
}
