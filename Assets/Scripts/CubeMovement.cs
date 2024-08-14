using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CubeMovement : MonoBehaviour
{
    public Transform redCube;
    public Transform greenCube;
    public GameObject spheresGroup;
    public TextMeshProUGUI distanceText;

    private float switchSceneDistance = 2f;
    private float showSpheresDistance = 4f;

    void Update()
    {
        HandleMovement();
        HandleDistanceCheck();
    }

    void HandleMovement()
    {
        // Red cube movement (WASD keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 redMovement = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        redCube.Translate(redMovement * Time.deltaTime * 5);

        redCube.position = new Vector3(
            Mathf.Clamp(redCube.position.x, -8f, 8f),
            redCube.position.y,
            Mathf.Clamp(redCube.position.z, -4f, 4f)
        );

        // Green cube movement (Arrow keys)
        float moveHorizontalArrow = Input.GetAxis("HorizontalArrow");
        float moveVerticalArrow = Input.GetAxis("VerticalArrow");
        Vector3 greenMovement = new Vector3(moveHorizontalArrow, 0, moveVerticalArrow).normalized;
        greenCube.Translate(greenMovement * Time.deltaTime * 5);

        greenCube.position = new Vector3(
            Mathf.Clamp(greenCube.position.x, -8f, 8f),
            greenCube.position.y,
            Mathf.Clamp(greenCube.position.z, -4f, 4f)
        );
    }

    void HandleDistanceCheck()
    {
        float distance = Vector3.Distance(redCube.position, greenCube.position);
        distanceText.text = "Distance between cubes: " + distance.ToString("F2") + " meters";

        if (distance < showSpheresDistance)
        {
            spheresGroup.SetActive(true);
        }
        else
        {
            spheresGroup.SetActive(false);
        }

        if (distance < switchSceneDistance)
        {
            SceneManager.LoadScene("VisualGraphScene");
        }
    }
}
