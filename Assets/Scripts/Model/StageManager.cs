using UnityEngine;

public interface IStageManager
{
    Quaternion TiltStage(float tiltSensitivity);
}

public class StageManager : IStageManager
{
    public Quaternion TiltStage(float tiltSensitivity)
    {
        Vector3 acceleration = Input.acceleration;
        float tiltX = acceleration.x * tiltSensitivity;
        float tiltY = acceleration.y * tiltSensitivity;
        return Quaternion.Euler(tiltY, 0, -tiltX);
    }
}