using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

public class ChatPage : Page
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private ChatPanel _chatPanel;
    [SerializeField] private int _startMessageCount = 20;
    [SerializeField] private UsersPanel _usersPanel;

    private bool _chatPanelInited;
    private bool _usersPanelInited;
    private SignalRConnector _connector;

    public override void Enter()
    {
        base.Enter();

        _loadingScreen.SetActive(true);

        _connector = App.Instance.Connector;
        _connector.StatusReceived += _usersPanel.UpdateList;
        _connector.MessageReceived += _chatPanel.AddMessage;

        _chatPanelInited = false;
        _usersPanelInited = false;
        StartCoroutine(InitChatPanel());
        StartCoroutine(InitUsersPanel());
    }

    [Serializable]
    public class MessageList
    {
        public List<Message> messages;
    }

    private IEnumerator InitChatPanel()
    {
        var request = UnityWebRequest
            .Get($"{App.Instance.ServerUrl}/api/messages/{_startMessageCount}");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var json = request.downloadHandler.text;
            var list = JsonUtility.FromJson<MessageList>(json);

            foreach (var message in list.messages)
            {
                _chatPanel.AddMessage(message);
            }

            _chatPanelInited = true;
            if (_chatPanelInited && _usersPanelInited)
                OnInitEnded();
        }
    }

    [Serializable]
    public class UserList
    {
        public List<User> users;
    }

    private IEnumerator InitUsersPanel()
    {
        var request = UnityWebRequest
            .Get($"{App.Instance.ServerUrl}/api/users");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var json = request.downloadHandler.text;
            var list = JsonUtility.FromJson<UserList>(json);

            foreach (var user in list.users)
            {
                _usersPanel.UpdateList(user);
            }

            _usersPanelInited = true;
            if (_chatPanelInited && _usersPanelInited)
                OnInitEnded();
        }
    }

    private void OnInitEnded()
    {
        _loadingScreen.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();

        StopAllCoroutines();

        _connector.MessageReceived -= _chatPanel.AddMessage;
        _chatPanel.Clear();

        _connector.StatusReceived -= _usersPanel.UpdateList;
        _usersPanel.Clear();

        _chatPanelInited = false;
        _usersPanelInited = false;
    }
}
