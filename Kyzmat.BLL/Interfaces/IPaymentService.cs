namespace Kyzmat.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> MakePaymentAsync(string userId);
    }
}
