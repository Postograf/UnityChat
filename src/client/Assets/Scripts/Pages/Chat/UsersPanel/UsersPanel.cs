using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class UsersPanel : Panel
{
    [SerializeField] private UserView _userPrefab;
    private List<UserView> users = new List<UserView>();

    public void UpdateList(User user)
    {
        var newUserColor = App.Instance.DefaultColor;
        ColorUtility.TryParseHtmlString(user.color, out newUserColor);

        var userView = users
            .FirstOrDefault(
                x => x.Name == user.name
                && x.Color == newUserColor
            );

        Debug.Log($"user: {user.name} with color: {newUserColor}");
        Debug.Log($"Consisted users: {string.Join("; ", users.Select(x => $"Name {x.Name}, Color {x.Color}"))}");

        if (userView is null)
        {
            var userColor = App.Instance.DefaultColor;
            ColorUtility.TryParseHtmlString(user.color, out userColor);

            var newUser = Instantiate(_userPrefab, _content);
            newUser.Init(user.name, userColor, user.status);
            users.Add(newUser);
        }
        else
        {
            userView.Status = user.status;
        }
    }

    public override void Clear()
    {
        base.Clear();

        users.Clear();
    }
}
