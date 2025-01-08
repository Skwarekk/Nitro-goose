using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerBackwardDrag;
    [SerializeField] private Vector2 playerSize;
    [SerializeField] private LayerMask wallsLayerMask;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0);

        Vector2 origin = new Vector3(transform.position.x, transform.position.y, 0);
        float correctSpeed = playerSpeed;
        if(gameInput.GetMovementVectorNormalized().x < 0)
        {
            correctSpeed = correctSpeed * playerBackwardDrag; 
        }
        if (!Physics2D.BoxCast(origin, playerSize, 0f, moveDir, playerSpeed * Time.deltaTime, wallsLayerMask))
        {
            transform.position += moveDir * correctSpeed * Time.deltaTime;
        }
    }
}
