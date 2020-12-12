using System.Threading.Tasks;

namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface IMailService
    {
        Task WyslijWadomoscEmail(string adresEmail, string temat, string tresc);
    }
}