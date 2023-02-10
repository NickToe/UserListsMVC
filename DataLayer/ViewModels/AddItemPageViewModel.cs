namespace UserListsMVC.DataLayer.ViewModels;

public class AddItemPageViewModel<T> : UserListViewModel
{
    public AddItemPageViewModel(string userName, ContentType contentType = ContentType.None) : base(userName, contentType) { }
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
