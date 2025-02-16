using System;
using UnityEngine;
//using UnityEngine.UI;
using UniRx;
using UnityEngine.UIElements;

public interface IGameInput
{
    IObservable<Unit> OnStartButtonClickAsObservable { get; }
    IObservable<Unit> OnQuitButtonClickAsObservable { get; }
    IObservable<Unit> OnRestartButtonClickAsObservable { get; }
    IObservable<Unit> OnBack2MenuButtonClickAsObservable { get; }
}

public class GameInput : MonoBehaviour, IGameInput
{
    [SerializeField] private UIDocument _startUIDocument;
    [SerializeField] private UIDocument _endUIDocument;

    private Button _startButton;
    private Button _quitButton;
    private Button _restartButton;
    private Button _back2MenuButton;

    public IObservable<Unit> OnStartButtonClickAsObservable { get; private set; }
    public IObservable<Unit> OnQuitButtonClickAsObservable { get; private set; }
    public IObservable<Unit> OnRestartButtonClickAsObservable { get; private set; }
    public IObservable<Unit> OnBack2MenuButtonClickAsObservable { get; private set; }

    private VisualElement _startUI;
    private VisualElement _endUI;

    private void Awake()
    {
        if (_startUIDocument == null || _endUIDocument == null)
        {
            Debug.LogError("UI ドキュメントが設定されていません。");
            return;
        }

        _startUI = _startUIDocument.rootVisualElement.Q<VisualElement>("StartUI");
        _endUI = _endUIDocument.rootVisualElement.Q<VisualElement>("EndUI");

        if (_startUI == null || _endUI == null)
        {
                Debug.LogError("UI要素 StartUI または EndUI が見つかりません。");
                return;
        }
        
        _startButton = _startUI.Q<Button>("start-button");
        _quitButton = _startUI.Q<Button>("quit-button");
        _restartButton = _endUI.Q<Button>("restart-button");
        _back2MenuButton = _endUI.Q<Button>("back2menu-button");

        if (_startButton == null || _quitButton == null || _restartButton == null || _back2MenuButton == null)
        {
            Debug.LogError("ボタン要素が見つかりません。");
            return;
        }

        OnStartButtonClickAsObservable = Observable.FromEvent(
            handler => _startButton.clicked += handler,
            handler => _startButton.clicked -= handler
        );
        OnQuitButtonClickAsObservable = Observable.FromEvent(
            handler => _quitButton.clicked += handler,
            handler => _quitButton.clicked -= handler
        );
        OnRestartButtonClickAsObservable = Observable.FromEvent(
            handler => _restartButton.clicked += handler,
            handler => _restartButton.clicked -= handler
        );
        OnBack2MenuButtonClickAsObservable = Observable.FromEvent(
            handler => _back2MenuButton.clicked += handler,
            handler => _back2MenuButton.clicked -= handler
        );
    }
}