using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public interface IGameInput
{
    IObservable<Unit> OnStartButtonClickAsObservable { get; }
    IObservable<Unit> OnQuitButtonClickAsObservable { get; }
    IObservable<Unit> OnRestartButtonClickAsObservable { get; }
    IObservable<Unit> OnBack2MenuButtonClickAsObservable { get; }
}

public class GameInput : MonoBehaviour, IGameInput
{
   // 各種ボタン
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _back2MenuButton;
    
    // 各種ボタンのIObservable
    public IObservable<Unit> OnStartButtonClickAsObservable
            => _startButton.OnClickAsObservable();
    public IObservable<Unit> OnQuitButtonClickAsObservable
            => _quitButton.OnClickAsObservable();
    public IObservable<Unit> OnRestartButtonClickAsObservable
            => _restartButton.OnClickAsObservable();
    public IObservable<Unit> OnBack2MenuButtonClickAsObservable
            => _back2MenuButton.OnClickAsObservable();
}