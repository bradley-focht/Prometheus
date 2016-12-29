using System;

namespace Prometheus.WebUI.Helpers
{
	public class FinancialCalculator
	{
		public FinancialCalculator(int years, double interest)
		{
			n = years;
			i = interest;
		}
		/// <summary>
		/// interest rate
		/// </summary>
		public double i { get; set; }
		/// <summary>
		/// n number of years
		/// </summary>
		public int n { get; set; }

		public double CalculatePw(double initial, double monthlySeries)
		{
			return initial + CalculatePw(monthlySeries, 12);
		}

		/// <summary>
		/// Calculate PW of uniform series
		/// </summary>
		/// <param name="seriesAmount"></param>
		/// <param name="compundingPeriod"></param>
		/// <returns></returns>
		public double CalculatePw(double seriesAmount, int compundingPeriod)
		{
			return seriesAmount * (Math.Pow(1 + i, n * compundingPeriod) - 1)/(i*Math.Pow(1 + i, n * compundingPeriod));
		}
	}
}