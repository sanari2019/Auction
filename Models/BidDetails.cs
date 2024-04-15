public class BidDetails
{
    public int id { get; set; }
    public Bid bid { get; set; } = new Bid();
    public int bidId { get; set; }
    public Vehicle Vehicle { get; set; } = new Vehicle();
    public int? vehicleId { get; set; }
    public int? itemId { get; set; }
    public Bidder bidder { get; set; } = new Bidder();
    public int bidderId { get; set; }
    public int staffId { get; set; }
    public decimal staffBid { get; set; }
    public DateTime bidDate { get; set; }
}