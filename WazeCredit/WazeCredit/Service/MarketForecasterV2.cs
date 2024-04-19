using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class MarketForecasterV2 : IMarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            // TODO: Call API
            // TODO: Calculate current stock market forecast

            // For this purpose, result is hard coded
            return new MarketResult
            {
                MarketCondition = MarketCondition.Volatile
            };
        }
    }
}
