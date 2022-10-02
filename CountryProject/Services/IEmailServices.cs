using CountryProject.Models;
using System.Threading.Tasks;

namespace CountryProject.Services
{
    public interface IEmailServices
    {
        bool SendMail(EmailModel model);
    }
}
