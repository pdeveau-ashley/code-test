public class CustomerManager : Manager
{
    public override IRepository Repository { get; }
    private readonly ILogger _logger;

    public record Customer(string Id, string SocialSecurityNumber, string City, string State, string PostalCode,
        string AddressLine1, string AddressLine2, ICollection<PersonalHealthInfo> PersonalHealthHistory);

    public CustomerManager(ILogger<CustomerManager> logger, IRepository repository) =>
        (logger, repository) = (_logger, Repository);

    public static Task LoadCustomer(string id, IRepository repository)
    {
        try
        {
            return repository.GetCustomer(id);
        }
        catch(Exception ex)
        {
            _logger.Trace($"Failed to get {id}");
            throw new Exception(ex);
        }
    }

    public Task SaveCustomer<T>(T customer) where T : class
    {
        return Repository.Save(T as Customer);
    }
}
