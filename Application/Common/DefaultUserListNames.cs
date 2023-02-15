using UserListsMVC.Domain.Enums;

namespace UserListsMVC.Application.Common;

public static class DefaultUserListNames
{
    private record FullListType(ContentType ContentType, UserListType UserListType);

    private static readonly Dictionary<FullListType, string> ListName = new();

    static DefaultUserListNames()
    {
        foreach (var contentTypeStr in Enum.GetNames(typeof(ContentType)))
        {
            foreach (var userListTypeStr in Enum.GetNames(typeof(UserListType)))
            {
                Enum.TryParse(contentTypeStr, out ContentType contentType);
                Enum.TryParse(userListTypeStr, out UserListType userListType);
                ListName.Add(new(contentType, userListType), $"{contentTypeStr} {userListTypeStr}");
            }
        }
    }

    public static string GetName(ContentType contentType, UserListType userListType)
    {
        return ListName[new(contentType, userListType)];
    }
}