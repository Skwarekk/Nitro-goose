using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerSpeed;

    private void Update()
    {
        transform.position += (Vector3)gameInput.GetMovementVectorNormalized() * playerSpeed * Time.deltaTime;
    }
}
