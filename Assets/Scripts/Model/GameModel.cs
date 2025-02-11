using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IGameModel
{
    bool IsGameStarted { get; set; }
    float RequiredContactTime { get; }
    float GameOverTime { get; }
    float GameTime { get; set; }
    IList<float> TopTimes { get; }
    string SaveFilePath { get; set; }
    ScoreData ScoreData { get; set; }
    float TiltSensitivity { get; set; }
    BallData[] BallDatas { get; set; }

    void ResetContactTimes();
}

// 初期値のコメントは省略
public class GameModel : IGameModel
{
    public bool IsGameStarted { get; set; } = false;
    public float RequiredContactTime { get; } = 3.0f;
    public float GameOverTime { get; } = 30.0f;
    public float GameTime { get; set; } = 0.0f;
    public IList<float> TopTimes { get; } = new List<float>();
    public string SaveFilePath { get; set; } = "";
    public ScoreData ScoreData { get; set; } = new ScoreData();
    public float TiltSensitivity { get; set; } = 20.0f;
    public BallData[] BallDatas { get; set; }

    public GameModel()
    {
        BallDatas = Enumerable.Range(0, 2)
            .Select(i => new BallData 
            { 
                StartingPosition = i == 0 ? 
                new Vector3(0, 1, 0) : 
                new Vector3(0.05f, 2, 0) 
            })
            .ToArray();
    }
    
    public void ResetContactTimes()
    {
        for (int i = 0; i < BallDatas.Length; i++)
        {
            BallDatas[i].ContactTime = 0.0f;
        }
    }
}