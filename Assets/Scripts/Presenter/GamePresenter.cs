using UnityEngine;
using UniRx;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _gameViewComponent;
    [SerializeField] private GameObject _playableBallPrefab;
    [SerializeField] private Transform _ballParentTransform;
    [SerializeField] private Transform _gameOverPanel;

    private IGameView _view;
    private IGameInput _input;
    private StageTiltController _tilt;

    private IGameModel _gameModel;
    private IBallFactory _ballFactory;
    private IStageManager _stageManager;
    private IScoreDataStorage _scoreDataStorage;

    private GameManager _gameManager;

    private void Awake()
    {
        InitializeDependencies();
        LoadGameData();
        InitializeView();
        InitializeGameManager();
    }

    private void Start()
    {
        Bind();
    }

    private void Update()
    {
        if (_gameModel.IsGameStarted)
        {
            _gameManager.Update(Time.deltaTime);
        }
    }
 
    private void Bind()
    {
        _input.OnStartButtonClickAsObservable
            .Subscribe(_ => StartGame()).AddTo(this);
        _input.OnQuitButtonClickAsObservable
            .Subscribe(_ => QuitGame()).AddTo(this);
        _input.OnRestartButtonClickAsObservable
            .Subscribe(_ => RestartGame()).AddTo(this);
        _input.OnBack2MenuButtonClickAsObservable
            .Subscribe(_ => Back2Menu()).AddTo(this);
    }

    private void InitializeDependencies()
    {
        _gameModel = new GameModel();
        _ballFactory = new BallFactory();
        _stageManager = new StageManager();
        _scoreDataStorage = new ScoreDataStorage();
    }
    private void LoadGameData()
    {
        _gameModel.SaveFilePath = Application.persistentDataPath + "/score_data.json";
        _gameModel.ScoreData = _scoreDataStorage.LoadScores(_gameModel.SaveFilePath);
    }

    private void InitializeView()
    {
        _view = _gameViewComponent.GetComponent<IGameView>();
        _input = _gameViewComponent.GetComponent<IGameInput>();
        _tilt = _gameViewComponent.GetComponent<StageTiltController>();
        if (_view == null || _input == null || _tilt == null)
        {
            Debug.LogError("ViewコンポーネントがIGameView, IGameInput, IGameMoveを実装していません");
            return;
        }
        _view.DisplayScores(_gameModel.ScoreData);
    }

    private void InitializeGameManager()
    {
        _gameManager = new GameManager(_gameModel, _ballFactory, _stageManager, _scoreDataStorage,_view,_tilt);
        _gameManager.SetGameOverHeight(_gameOverPanel.position.y);
        _gameManager.SetBallParent(_ballParentTransform);
        _gameManager.SetBallPrefab(_playableBallPrefab);
    }

    private void StartGame()
    {
        _gameModel.IsGameStarted = true;
        _gameModel.ResetContactTimes();
        _view.HidePlayerRankUI();
        _view.HideStartUI();
        _view.ShowGameStage();
        _gameManager.CreateBall();
    }

    private void RestartGame()
    {
        _gameModel.IsGameStarted = true;
        _view.HideEndGameCommonUI();
        _view.HideGameClearUI();
        _view.HideGameOverUI();
        _view.HidePlayerRankUI();
        _gameManager.CreateBall();
    }

    private void Back2Menu()
    {
        _view.HideEndGameCommonUI();
        _view.HideGameClearUI();
        _view.HideGameOverUI();
        _view.HideGameStage();
        _view.ShowStartUI();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}