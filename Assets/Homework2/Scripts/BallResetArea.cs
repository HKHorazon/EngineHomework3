using UnityEngine;

public class BallResetArea : MonoBehaviour
{
    [field:SerializeField] public bool hasBallInArea { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>() != null)
        {
            hasBallInArea = true;
        }

    }

    public void ResetState()
    {

        hasBallInArea = false;
    }
}
