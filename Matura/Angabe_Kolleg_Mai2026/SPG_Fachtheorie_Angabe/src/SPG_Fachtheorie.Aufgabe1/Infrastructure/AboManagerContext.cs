using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure;

public class AboManagerContext : DbContext
{
    public DbSet<PaymentLog> PaymentLogs => Set<PaymentLog>();
    public DbSet<PaymentStrategy> PaymentStrategies => Set<PaymentStrategy>();
    public DbSet<PeriodicPayment> PeriodicPayments => Set<PeriodicPayment>();
    public DbSet<OneTimePayment> OneTimePayments => Set<OneTimePayment>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public AboManagerContext(DbContextOptions options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: Add your implementation
    }
}
