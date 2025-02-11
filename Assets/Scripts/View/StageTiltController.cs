using UnityEngine;

public interface IStageTiltController
{
    void SetRotation(Quaternion tiltRotation);
}

public class StageTiltController : MonoBehaviour, IStageTiltController
{
    [SerializeField] private Transform _stage;
    public void SetRotation(Quaternion tiltRotation)
    {
        _stage.rotation = tiltRotation;
    }
}