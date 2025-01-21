using System;

namespace SalesDatePrediction.Models
{
    public class PredictedOrder
    {
        public int Custid { get; set; }
        public string CustomerName { get; set; }

        public DateTime LastOrderDate { get; set; }

        public DateTime NextPredictedOrder { get; set; }
    }
}