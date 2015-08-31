using Artech.ServiceDiscovery.Service.Interface;
namespace Artech.ServiceDiscovery.Service
{
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }
    }
}
