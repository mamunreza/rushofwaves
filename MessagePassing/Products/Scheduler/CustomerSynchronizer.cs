using MessagePassing.Products.Data;
using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.Scheduler;

internal interface ICustomerSynchronizer
{
    public Task SynchronizeCustomerAddedAsync(CancellationToken cancellationToken);
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
        var processedCustomerIds = await _appDbContext.Customers
            .Where(c => inboxItems.Select(x => x.RootId).Contains(c.Id))
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);

        foreach (var inboxItem in inboxItems)
        {
            if (processedCustomerIds.Contains(inboxItem.RootId))
            {
                _logger.LogInformation("Customer already processed, id: {CustomerId}", inboxItem.RootId);
            }
            else
            {
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
                _appDbContext.Customers.Add(customer);
            }
            inboxItem.IsProcessed = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        _logger.LogInformation("Synchronization ended for customer added");
    }
}
