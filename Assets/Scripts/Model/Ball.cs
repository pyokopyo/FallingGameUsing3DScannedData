// BallクラスはGameObjectを保持するのみ。
// その他の処理はBallManagerに委譲。

using UnityEngine;

public class Ball
{
    public GameObject GameObject { get; }

    public Ball(GameObject gameObject)
    {
        GameObject = gameObject;
    }
}