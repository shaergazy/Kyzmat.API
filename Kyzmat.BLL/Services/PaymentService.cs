using Kyzmat.BLL.Configs;
using Kyzmat.BLL.Interfaces;
using Kyzmat.DAL.Interfaces;
using Kyzmat.DAL.Models;
using Microsoft.Extensions.Options;

namespace Kyzmat.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly PaymentSettings _settings;

        public PaymentService(IUnitOfWork uow, IOptions<PaymentSettings> options)
        {
            _uow = uow;
            _settings = options.Value;
        }

        public async Task<bool> MakePaymentAsync(string userId)
        {
            var amount = _settings.ChargeAmount;

            using var transaction = await _uow.BeginTransactionAsync();

            try
            {
                var userRepo = _uow.GetRepository<User>();
                var user = await userRepo.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

                if (user == null)
                    throw new InvalidOperationException("User not found");

                if (user.Balance < amount)
                    throw new InvalidOperationException("insufficient balance");

                user.Balance -= amount;

                var paymentRepo = _uow.GetRepository<Payment>();
                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow
                };
                await paymentRepo.AddAsync(payment);
                await _uow.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}
