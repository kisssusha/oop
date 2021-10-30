using System.Collections.Generic;
using Backups.Models;

namespace Backups.Services
{
    public interface IAlgorithmic
    {
        List<Archive> StartAlgorithmic(JobObjects jobObjects, Repository repository);
    }
}