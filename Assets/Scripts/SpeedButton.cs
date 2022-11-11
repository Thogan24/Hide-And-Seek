using UnityEngine;

public class SpeedButton : MonoBehaviour
{
    public Player playerScript;
    public GameObject player;
    
    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        
        playerScript.speed = 22;
        playerScript.jumpHeight = 10;
    }
}
