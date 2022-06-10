namespace OrderProcessor
{
    using System;
    using System.Collections.Generic;

    public partial class OrderEvent2
    {
        public string Source { get; set; }
        public Details2 Details { get; set; }
    }

    public partial class Details2
    {
        public string? MerchantRef { get; set; }
        public double? Tax { get; set; }
    }
}
