using UnityEngine;
using static GameManager;

public class SlalomFlag : MonoBehaviour
{
    public static event TimerEvent RacePenalty;
    private enum Direction { Left, Right };

    [SerializeField] private Direction flagDirection;
    [SerializeField] private Material rightColor, notrightColor;
    private bool flagPassed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerPos != null && 
            PlayerController.playerPos.position.z < transform.position.z && !flagPassed)
        {
            flagPassed = true;
            Direction passingDirection = Direction.Right;
            if (PlayerController.playerPos.position.x < transform.position.x)
                passingDirection = Direction.Left;
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (passingDirection == flagDirection)
            {
                renderer.material = rightColor;
            }
            else
            {
                renderer.material = notrightColor;
                RacePenalty.Invoke();
            }
        }
    }
}
