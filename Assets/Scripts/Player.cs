using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private void Update()
    {
        Vector2 inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        inputVector = inputVector.normalized;

        transform.position += (Vector3)inputVector * playerSpeed * Time.deltaTime;
    }
}
