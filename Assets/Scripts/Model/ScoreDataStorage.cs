using System.Linq;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public interface IScoreDataStorage
{
    void SaveTopTimes(float gameClearTime, string saveFilePath);
    ScoreData LoadScores(string saveFilePath);
}

[System.Serializable]
public class ScoreData
{
    public List<PlayerData> players = new List<PlayerData>();
}

[System.Serializable]
public class PlayerData
{
    public float playerScore = 100f;
}

public class ScoreDataStorage : IScoreDataStorage
{
    public void SaveTopTimes(float gameClearTime, string saveFilePath)
    {
        SaveScore(gameClearTime, saveFilePath);
    }

    private void SaveScore(float playerScore, string saveFilePath)
    {
        ScoreData data = LoadScores(saveFilePath);

        // 新しいプレイヤーデータを追加
        data.players.Add(new PlayerData { playerScore = playerScore });

        // スコアの高い順にソートし、上位3つに絞る
        data.players = data.players.OrderBy(p => p.playerScore).Take(3).ToList();

        // JSON形式で保存
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public ScoreData LoadScores(string saveFilePath)
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<ScoreData>(json);
        }
        else
        {
            return new ScoreData(); // 初期データ
        }
    }
}
