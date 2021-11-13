using System.Collections.Generic;

namespace Banks.Models
{
    public class PercentOfDeposit
    {
        public PercentOfDeposit()
        {
            Percents = new List<(int sum, double percent)>();
        }

        public List<(int sum, double percent)> Percents { get; }

        public void AddPercentAndSum(int sum, double percent)
        {
            Percents.Add((sum, percent));
        }

        public void SortPercents()
        {
            Percents.Sort();
        }
    }
}