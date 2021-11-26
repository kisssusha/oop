using BackupsExtra.Models;

namespace BackupsExtra.Services
{
    public interface IClear
    {
        public bool IsLimitExceeded();
    }
}