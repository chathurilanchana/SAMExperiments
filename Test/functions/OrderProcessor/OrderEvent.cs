namespace OrderProcessor
{
    using System;
    using System.Collections.Generic;

    public partial class OrderEvent
    {
        public string Account { get; set; }
        public Created Detail { get; set; }
        public string DetailType { get; set; }
        public string Id { get; set; }
        public string Region { get; set; }
        public System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> Resources { get; set; }
        public string Source { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Version { get; set; }
    }

    public partial class Created
    {
        public double Amount { get; set; }
        public string CorelationId { get; set; }
        public string CustomerId { get; set; }
        public Details Details { get; set; }
    }

    public partial class Details
    {
        public string? MerchantRef { get; set; }
        public double? Tax { get; set; }
    }
}
