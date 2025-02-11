using UnityEngine;

public class GameManager
{
    private IGameModel _gameModel;
    private IBallFactory _ballFactory;
    private IStageManager _stageManager;
    private IScoreDataStorage _scoreDataStorage;
    private IGameView _view; 
    private IStageTiltController _tilt;
    private IGameRule _gameRule;
    private float _gameOverHeight;
    
    public GameManager(IGameModel gameModel, IBallFactory ballFactory, IStageManager stageManager, IScoreDataStorage scoreDataStorage,
    IGameView view, IStageTiltController tilt)
    {
        _gameModel = gameModel;
        _ballFactory = ballFactory;
        _stageManager = stageManager;
        _scoreDataStorage = scoreDataStorage;
        _gameRule = new GameRule(gameModel);
        _view = view;
        _tilt = tilt;
    }

    public void SetGameOverHeight(float gameOverPositionY)
    {
        _gameOverHeight = gameOverPositionY;
    }

    public void Update(float deltaTime)
    {
        _tilt.SetRotation(_stageManager.TiltStage(_gameModel.TiltSensitivity));
        _gameModel.GameTime += deltaTime;
        // 要素に代入するためforループを使用
        for (int i = 0; i < _gameModel.BallDatas.Length; i++)
        {
            _gameModel.BallDatas[i].ContactTime = _ballFactory.CheckBallContactTime(_gameModel.BallDatas[i].Ball, _gameModel.BallDatas[i].ContactTime);
        }

        if (_gameRule.IsGameOver(_gameOverHeight))
        {
            GameOver("ボールが落下");
        }

        if (_gameModel.GameTime >= _gameModel.GameOverTime)
        {
            GameOver("時間切れ");
        }

        if (_gameRule.IsGameClear())
        {
            GameClear();
        }
    }

    private void GameOver(string reason){
        _view.ShowGameOverUI();
        EndGameProcess("Game Over! (" + reason + ")");
    }

    private void GameClear()
    {
        _scoreDataStorage.SaveTopTimes(_gameModel.GameTime, _gameModel.SaveFilePath);
        _gameModel.ScoreData = _scoreDataStorage.LoadScores(_gameModel.SaveFilePath);
        _view.DisplayScores(_gameModel.ScoreData);
        _view.ShowGameClearUI();
        _view.UpdateGameTime(_gameModel.GameTime);
        EndGameProcess("Game Cleared!");
    }

    private void EndGameProcess(string msg)
    {
        _gameModel.IsGameStarted = false;
        _view.ShowPlayerRankUI();
        _view.ShowEndGameCommonUI();
        _gameModel.GameTime = 0.0f;
        // _gameModel.Balls[i] = null;を行うためforループを使用
        for (int i = 0; i < _gameModel.BallDatas.Length; i++)
        {
            _ballFactory.DestroyBall(_gameModel.BallDatas[i].Ball);
            _gameModel.BallDatas[i].Ball = null;
        }
        Debug.Log(msg);
    }

    private Transform _ballParentTransform;
    public void SetBallParent(Transform ballParentTransform)
    {
        _ballParentTransform = ballParentTransform;
    }

    private GameObject _playableBallPrefab;
    public void SetBallPrefab(GameObject playableBallPrefab)
    {
        _playableBallPrefab = playableBallPrefab;
    }

    public void CreateBall()
    {
        for (int i = 0; i < _gameModel.BallDatas.Length; i++)
        {
            _gameModel.BallDatas[i].Ball = _ballFactory.CreateBall(_playableBallPrefab, _ballParentTransform, _gameModel.BallDatas[i].StartingPosition).GameObject;
        }
    }
}