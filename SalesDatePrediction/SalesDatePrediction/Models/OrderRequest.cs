﻿using System;

namespace SalesDatePrediction.Models
{
    public class OrderRequest
    {
        public int Empid { get; set; }
        public int Custid { get; set; }
        public int Shipperid { get; set; }
        public string Shipname { get; set; }
        public string Shipaddress { get; set; }
        public string Shipcity { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime Requireddate { get; set; }
        public DateTime? Shippeddate { get; set; }
        public decimal Freight { get; set; }
        public string Shipcountry { get; set; }
        public int Productid { get; set; }
        public decimal Unitprice { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }
    }

}