using UnityEngine;

public class BallCollisionHandler  : MonoBehaviour
{
    private bool isTouchingFloor = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isTouchingFloor = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isTouchingFloor = false;
        }
    }

    public bool IsTouchingFloor()
    {
        return isTouchingFloor;
    }
}