using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class UserList<T> where T : UserListItemBase
{
    public UserList(string listName, ContentType contentType, UserListType listType)
    {
        Name = listName;
        ContentType = contentType;
        ListType = listType;
    }

    public UserList(ContentType contentType, UserListType listType)
    {
        Name = DefaultUserListNames.GetName(contentType, listType);
        ContentType = contentType;
        ListType = listType;
    }

    [Key]
    public int UserListId { get; set; }
    public string Name { get; set; }
    public bool IsPublic { get; set; } = true;
    public ContentType ContentType { get; set; }
    public UserListType ListType { get; set; }

    public ICollection<T> UserListItems { get; set; } = null!;

    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public int Count => UserListItems.Count;
    public bool Any(string itemId) => UserListItems.Any(item => item.ItemId == itemId);
    public T? Find(string itemId) => UserListItems.FirstOrDefault(item => item.ItemId == itemId);
    public void Add(T item)
    {
        item.Position = Count + 1;
        UserListItems.Add(item);
    }

    public bool Remove(T item)
    {
        RemovePosition(item.Position);
        return UserListItems.Remove(item);
    }

    private void RemovePosition(int removedPosition)
    {
        var collection = UserListItems.Where(item => item.Position > removedPosition).OrderBy(item => item.Position);
        foreach (var item in collection)
        {
            item.Position = item.Position - 1;
        }
    }

    public void Update(T itemOld, T itemNew)
    {
        if (itemNew.Position > 0 && itemNew.Position <= Count)
        {
            if (itemOld.Position != itemNew.Position)
            {
                ChangePositions(itemOld.Position, itemNew.Position);
            }
            itemOld.Update(itemNew);
        }
    }

    private void ChangePositions(int oldPosition, int newPosition)
    {
        var predicateCondition = oldPosition > newPosition ? new Func<T, bool>(item => item.Position >= newPosition && item.Position < oldPosition) : (item => item.Position > oldPosition && item.Position <= newPosition);
        var predicateOperation = oldPosition > newPosition ? new Func<int, int>(x => x + 1) : (x => x - 1);
        var collection = UserListItems.Where(predicateCondition).OrderBy(item => item.Position);
        foreach (var item in collection)
        {
            item.Position = predicateOperation(item.Position);
        }
    }

    public void UpdatePrivacy(bool isPublic) => IsPublic = isPublic;
}