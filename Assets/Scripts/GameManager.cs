using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public int unitsForPixel = 100;
    public float halfScreenWidth = Screen.width / 2;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one instance of GameManager");
        }
        Instance = this;
    }
}
