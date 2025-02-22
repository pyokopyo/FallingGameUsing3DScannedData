
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

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

    [SerializeField] private UIDocument _commonUIDocument;
    [SerializeField] private UIDocument _startUIDocument;
    [SerializeField] private UIDocument _endUIDocument;
    [SerializeField] private GameObject _gameStage;

    private VisualElement _commonUIroot;
    private VisualElement _startUIroot;
    private VisualElement _endUIroot;
    private VisualElement _startUI;
    private VisualElement _gameClearUI;
    private VisualElement _gameOverUI;
    private VisualElement _endGameCommonUI;
    private Label _rank1;
    private Label _rank2;
    private Label _rank3;
    private Label _clearTime;

    // UI表示/非表示メソッドの実装
    private void Awake()
    {
        _startUIroot = _startUIDocument.rootVisualElement;
        _startUI = _startUIroot.Q<VisualElement>("StartUI");
        _commonUIroot = _commonUIDocument.rootVisualElement.Q<VisualElement>("Common");
        _rank1 = _commonUIroot.Q<Label>("rank_1");
        _rank2 = _commonUIroot.Q<Label>("rank_2");
        _rank3 = _commonUIroot.Q<Label>("rank_3");
        _endUIroot = _endUIDocument.rootVisualElement;
        _gameClearUI = _endUIroot.Q<VisualElement>("GameClear");
        _gameOverUI = _endUIroot.Q<VisualElement>("GameOver");
        _clearTime = _endUIroot.Q<Label>("clear_time");
        _endGameCommonUI = _endUIroot.Q<VisualElement>("Common");
    }

    public void ShowStartUI() 
    {
        _startUI.style.opacity = 1f;
        _startUI.pickingMode = PickingMode.Position;
        foreach (var button in _startUI.Query<Button>().ToList())
        {
            button.pickingMode = PickingMode.Position;
        }
    }
    public void HideStartUI()
    {
        _startUI.style.opacity = 0f;
        _startUI.pickingMode = PickingMode.Ignore;
        foreach (var button in _startUI.Query<Button>().ToList())
        {
            button.pickingMode = PickingMode.Ignore;
        }
    }
    public void ShowGameStage() { _gameStage.SetActive(true); }
    public void HideGameStage() { _gameStage.SetActive(false); }
    public void ShowGameClearUI() 
    {
        _gameClearUI.style.opacity = 1f;
    }
    public void HideGameClearUI()
    {
        _gameClearUI.style.opacity = 0f;
    }
    public void ShowGameOverUI()
    {
        _gameOverUI.style.opacity = 1f;
    }
    public void HideGameOverUI()
    {
        _gameOverUI.style.opacity = 0f;
    }
    public void ShowEndGameCommonUI()
    {
        _endGameCommonUI.style.opacity = 1f;
        _endGameCommonUI.pickingMode = PickingMode.Position;
        foreach (var button in _endGameCommonUI.Query<Button>().ToList())
        {
            button.pickingMode = PickingMode.Position;
        }
    }
    public void HideEndGameCommonUI()
    {
        _endGameCommonUI.style.opacity = 0f;
        _endGameCommonUI.pickingMode = PickingMode.Ignore;
        foreach (var button in _endGameCommonUI.Query<Button>().ToList())
        {
            button.pickingMode = PickingMode.Ignore;
        }
    }
    public void ShowPlayerRankUI()
    {
        _commonUIroot.style.opacity = 1f;
    }
    public void HidePlayerRankUI()
    {
        _commonUIroot.style.opacity = 0f;
    }

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
        _clearTime.text = $"Clear Time: {gameTime:F3}";
    }
}