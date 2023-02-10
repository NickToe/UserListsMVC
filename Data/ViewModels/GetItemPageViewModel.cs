namespace UserListsMVC.DataLayer.ViewModels;

public class GetItemPageViewModel<T>
{
    public GetItemPageViewModel(T item, ItemInfo itemDetails)
    {
        Item = item;
        ItemInfo = itemDetails;
    }

    public T Item { get; set; }
    public ItemInfo ItemInfo { get; set; }
}
