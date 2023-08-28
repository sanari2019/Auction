public class BidDetails
{
    public int id { get; set; }
    public Bid bid { get; set; }
    public int bidId { get; set; }
    public Vehicle Vehicle { get; set; }
    public int vehicleId { get; set; }
    public Bidder bidder { get; set; }
    public int bidderId { get; set; }
    public decimal staffBid { get; set; }
    public DateTime bidDate { get; set; }
}