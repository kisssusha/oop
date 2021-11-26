using System.Collections.Generic;
using BackupsExtra.Models;

namespace BackupsExtra.Services
{
    public interface IAlgorithmic
    {
        List<Storage> StartAlgorithmic(JobObjects jobObjects, Repository repository);
    }
}