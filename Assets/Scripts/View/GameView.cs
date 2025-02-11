
using UnityEngine;
using TMPro;

public interface IGameView
{
    // UIの表示/非表示
    void ShowStartUI();
    void HideStartUI();
    void ShowGameStage();
    void HideGameStage();
    void ShowGameClearUI();
    void HideGameClearUI();
    void ShowGameOverUI();
    void HideGameOverUI();
    void ShowEndGameCommonUI();
    void HideEndGameCommonUI();
    void ShowPlayerRankUI();
    void HidePlayerRankUI();

    // スコア表示
    void DisplayScores(ScoreData scoreData);

    // 各種UI要素の更新
    void UpdateGameTime(float gameTime);
}

public class GameView : MonoBehaviour, IGameView
{
    [SerializeField] private GameObject _startUI;
    [SerializeField] private GameObject _gameStage;
    [SerializeField] private GameObject _gameClearUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _endGameCommonUI;
    [SerializeField] private GameObject _playerRankUI;

    [SerializeField] private TMP_Text _rank1;
    [SerializeField] private TMP_Text _rank2;
    [SerializeField] private TMP_Text _rank3;
    [SerializeField] private TMP_Text _gameTime;

    // UI表示/非表示メソッドの実装
    public void ShowStartUI() { _startUI.SetActive(true); }
    public void HideStartUI() { _startUI.SetActive(false); }
    public void ShowGameStage() { _gameStage.SetActive(true); }
    public void HideGameStage() { _gameStage.SetActive(false); }
    public void ShowGameClearUI() { _gameClearUI.SetActive(true); }
    public void HideGameClearUI() { _gameClearUI.SetActive(false); }
    public void ShowGameOverUI() { _gameOverUI.SetActive(true); }
    public void HideGameOverUI() { _gameOverUI.SetActive(false); }
    public void ShowEndGameCommonUI() { _endGameCommonUI.SetActive(true); }
    public void HideEndGameCommonUI() { _endGameCommonUI.SetActive(false); }
    public void ShowPlayerRankUI() { _playerRankUI.SetActive(true); }
    public void HidePlayerRankUI() { _playerRankUI.SetActive(false); }

    public void DisplayScores(ScoreData scoreData)
    {
        if (scoreData.players.Count > 0)
        {
            _rank1.text = $"{1}: {scoreData.players[0].playerScore:F3}";
        }
        else
        {
            _rank1.text = "1. -";
        }

        if (scoreData.players.Count > 1)
        {
            _rank2.text = $"{2}: {scoreData.players[1].playerScore:F3}";
        }
        else
        {
            _rank2.text = "2. -";
        }

        if (scoreData.players.Count > 2)
        {
            _rank3.text = $"{3}: {scoreData.players[2].playerScore:F3}";
        }
        else
        {
            _rank3.text = "3. -";
        }
    }
    public void UpdateGameTime(float gameTime)
    {
        _gameTime.text = $"Clear Time: {gameTime:F3}";
    }
}