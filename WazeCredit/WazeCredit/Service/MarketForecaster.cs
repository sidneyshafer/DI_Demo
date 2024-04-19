using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class MarketForecaster : IMarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            // TODO: Call API
            // TODO: Calculate current stock market forecast

            // For this purpose, result is hard coded
            return new MarketResult
            {
                MarketCondition = MarketCondition.StableUp
            };
        }
    }
}
