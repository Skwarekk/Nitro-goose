using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerBackwardDrag;
    [SerializeField] private float playerSize;
    [Space]
    [Header("Collision settings")]
    [Space]
    [SerializeField] private Vector2 playerColliderSize;
    [SerializeField] private LayerMask wallsLayerMask;

    private void Awake()
    {
        transform.localScale = new Vector3(playerSize, playerSize, playerSize);
    }

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
        if (!Physics2D.BoxCast(origin, playerColliderSize, 0f, moveDir, playerSpeed * Time.deltaTime, wallsLayerMask))
        {
            transform.position += moveDir * correctSpeed * Time.deltaTime;
        }
    }
}
