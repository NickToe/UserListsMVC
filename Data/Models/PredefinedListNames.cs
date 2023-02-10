namespace UserListsMVC.DataLayer.Models;

public static class PredefinedListNames
{
    static PredefinedListNames()
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
    private static readonly Dictionary<FullListType, string> ListName = new();

    public static string GetName(FullListType listType)
    {
        return ListName[listType];
    }
    public static string GetName(ContentType contentType, UserListType userListType)
    {
        return ListName[new(contentType, userListType)];
    }
}
