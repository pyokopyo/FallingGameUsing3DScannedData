using UnityEngine;

public interface IBallFactory
{
    Ball CreateBall(GameObject prefab, Transform parent, Vector3 position);
    void DestroyBall(GameObject ballGameObject);
    float CheckBallContactTime(GameObject ballGameObject, float currentContactTime);
}

public class BallData
{
    public GameObject Ball { get; set; }
    public Vector3 StartingPosition  { get; set; }
    public float ContactTime  { get; set; }
}

public class BallFactory : IBallFactory
{
    public Ball CreateBall(GameObject playableBallPrefab, Transform ballParentTransform, Vector3 position)
    {
        GameObject ballGameObject = GameObject.Instantiate(playableBallPrefab, position, Quaternion.identity, ballParentTransform);
        Ball ball = new Ball(ballGameObject);
        return ball;
    }

    public void DestroyBall(GameObject ballGameObject)
    {
        if (ballGameObject != null)
        {
            GameObject.Destroy(ballGameObject);
        }
    }

    public float CheckBallContactTime(GameObject ballGameObject, float currentContactTime)
    {
        if (ballGameObject != null && ballGameObject.GetComponent<BallCollisionHandler>().IsTouchingFloor())
        {
            currentContactTime += Time.deltaTime;
        }
        else
        {
            currentContactTime = 0.0f;
        }

        return currentContactTime;
    }
}