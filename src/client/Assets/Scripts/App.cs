using System;
using System.Threading.Tasks;

using UnityEngine;

public enum PageType : short
{
    Authorization,
    Chat,
    Loading
}

[Serializable]
public class PageTypePageDictionary : SerializableDictionary<PageType, Page> { }

public class App : MonoBehaviour
{
    [SerializeField] private PageTypePageDictionary _pages;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private string _serverUrl;

    public static App Instance { get; private set; }
    public User User { get; private set; }
    public SignalRConnector Connector { get; private set; }
    public Color DefaultColor => _defaultColor;
    public string ServerUrl => _serverUrl;

    private PageType _currentPage;
    public PageType CurrentPage
    {
        get => _currentPage;
        private set
        {
            _pages[_currentPage].Exit();
            _currentPage = value;
            _pages[_currentPage].Enter();
        }
    }

    private void Awake()
    {
        if (Instance is null)
            Instance = this;

        foreach (var keyValuePair in _pages)
            keyValuePair.Value.gameObject.SetActive(false);

        CurrentPage = PageType.Authorization;
    }

    public async void LogInAsync(User user)
    {
        User = user;

        CurrentPage = PageType.Loading;

        Connector = new SignalRConnector();
        Connector.Closed += OnConnectionClosed;
        Connector.Reconnected += OnReconnected;
        await Connector.InitAsync(ServerUrl, DefaultColor);
        await Connector.ChangeStatusAsync(User);

        CurrentPage = PageType.Chat;
    }

    private void OnConnectionClosed()
    {
        _pages[_currentPage].Exit();
        _pages[PageType.Loading].Enter();
    }

    private void OnReconnected()
    {
        _pages[PageType.Loading].Exit();
        _pages[_currentPage].Enter();
    }

    private async void OnApplicationQuit()
    {
        _pages[_currentPage].Exit();

        if (User != null)
        {
            User.status = false;
            await Connector?.ChangeStatusAsync(User);
        }
    }
}
