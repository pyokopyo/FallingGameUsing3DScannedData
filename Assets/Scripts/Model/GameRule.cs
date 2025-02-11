public interface IGameRule
{
    bool IsGameOver(float gameOverPositionY);
    bool IsGameClear();
}

public class GameRule : IGameRule
{
    private IGameModel _gameModel;

    public GameRule(IGameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public bool IsGameOver(float gameOverPositionY)
    {
        // いずれかのボールがgameOverPositionYより小さいか、
        // またはゲーム時間がGameOverTimeを超えたらゲームオーバー
        foreach (var ballData in _gameModel.BallDatas)
        {
            if (ballData.Ball != null && ballData.Ball.transform.position.y < gameOverPositionY)
            {
                return true; 
            }
        }

        return _gameModel.GameTime >= _gameModel.GameOverTime; // いずれのボールも条件を満たさなければ、ゲーム時間で判定
    }

    public bool IsGameClear()
    {
        // すべてのボールが RequiredContactTime 以上の接触時間を持っていればゲームクリア
        foreach (var ballData in _gameModel.BallDatas)
        {
            if (ballData.ContactTime < _gameModel.RequiredContactTime)
            {
                return false;
            }
        }

        return true; // すべてのボールが条件を満たしていればtrue
    }
}