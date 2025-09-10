using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kyzmat.DAL.Models
{
    public class User : IdentityUser<Guid>
    {
        public decimal Balance { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
