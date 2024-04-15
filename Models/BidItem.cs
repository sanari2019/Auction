    public class BidItem
    {
        public int id { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public string takeNote { get; set; }
        public int quantity { get; set; }
        public string? status { get; set; }
        public int bidid { get; set; }
        public Bid bid { get; set; }
        public bool active { get; set; }
    }
