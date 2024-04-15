 public class ItemImage
{
    public int id { get; set; }
    public int itemId { get; set; }
    public BidItem bidItem { get; set; }
    public string imageUrl { get; set; }
    public bool defaultImage { get; set; }
}