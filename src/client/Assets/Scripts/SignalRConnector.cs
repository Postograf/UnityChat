using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

public class SignalRConnector
{
    private HubConnection connection;

    public event Action<string, Color, string> MessageReceived;
    public event Action<User> StatusReceived;
    public event Action Reconnected;
    public event Action Closed;

    public async Task InitAsync(
        string serverUrl, 
        Color defaultColor
    )
    {
        connection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{serverUrl}/chatHub"))
            .WithAutomaticReconnect()
            .Build();
        
        connection.On<string, string, string>(
            "ReceiveMessage",
            (name, color, message) =>
            {
                Debug.Log("Message received");
                var messageColor = defaultColor;
                ColorUtility.TryParseHtmlString(color, out messageColor);

                MessageReceived
                    ?.Invoke(name, messageColor, message);
            }
        );
        
        connection.On<string, string, bool>(
            "ReceiveStatus",
            (name, color, status) =>
            {
                Debug.Log("Status received");
                StatusReceived?.Invoke(
                    new User
                    {
                        name = name,
                        color = color,
                        status = status
                    }
                );
            }
        );

        connection.Closed += (exception) =>
        {
            Debug.LogError($"connection closed: {exception.Message}");
            Closed?.Invoke();
            return null;
        };

        connection.Reconnected += (message) =>
        {
            Reconnected?.Invoke();
            return null;
        };

        await StartConnectionAsync();
    }

    public async Task SendMessageAsync(User user, string message)
    {
        try
        {
            await connection.InvokeAsync(
                "SendMessage", user.name, user.color, message
            );
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error {ex.Message}");
        }
    }

    public async Task ChangeStatusAsync(User user)
    {
        try
        {
            await connection.InvokeAsync(
                "ChangeStatus", 
                user.name, 
                user.color, 
                user.status
            );
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error {ex.Message}");
        }
    }

    private async Task StartConnectionAsync()
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error {ex.Message}");
        }
    }
}
