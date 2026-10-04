using UnityEngine;

public class GameManagerHW2 : MonoBehaviour
{
    private BallResetArea resetArea;
    private BallScript ball;
    private PinScript[] pins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = GameObject.FindAnyObjectByType<BallScript>();
        resetArea = GameObject.FindAnyObjectByType<BallResetArea>();
        pins = GameObject.FindObjectsByType<PinScript>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball?.ThrowBall();
        }
        if(resetArea.hasBallInArea 
            && Input.GetKeyDown(KeyCode.R))
        {
            ball?.ResetState();
            resetArea?.ResetState();
            if (pins!=null)
            {
                foreach (var pin in pins)
                {
                    pin?.ResetState();
                }
            }
        }
    }
}
