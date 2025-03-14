using MessagePassing.Products.Data;
using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.Scheduler;

internal interface ICustomerSynchronizer
{
    Task SynchronizeCustomerAddedAsync(CancellationToken cancellationToken);
}

internal class CustomerSynchronizer : ICustomerSynchronizer
{
    private readonly ILogger<CustomerSynchronizer> _logger;
    private readonly AppDbContext _appDbContext;

    public CustomerSynchronizer(
        ILogger<CustomerSynchronizer> logger,
        AppDbContext appDbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _appDbContext = appDbContext
            ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public async Task SynchronizeCustomerAddedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Synchronization started for customer added");

        var inboxItems = await _appDbContext.CustomerAddedInboxes.Where(x => !x.IsProcessed).Take(100).ToListAsync(cancellationToken);
        foreach (var inboxItem in inboxItems)
        {
            // Synchronize customer added
            var customer = new Customer
            {
                Id = inboxItem.RootId,
                FirstName = inboxItem.FirstName,
                LastName = inboxItem.LastName,
                Email = inboxItem.Email,
                Phone = inboxItem.Phone,
                CreatedAt = inboxItem.CreatedAt,
                UpdatedAt = inboxItem.UpdatedAt
            };
            inboxItem.IsProcessed = true;
            _appDbContext.Customers.Add(customer);
        }
        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Synchronization ended for customer added");
    }
}
