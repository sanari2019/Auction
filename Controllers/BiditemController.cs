using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
 public class BidItemsController : ControllerBase
{
    private readonly BidItemRepository _bidItemRepository;

    public BidItemsController(BidItemRepository bidItemRepository)
    {
        _bidItemRepository = bidItemRepository;
    }

    // GET: api/BidItems
    [HttpGet]
    public IActionResult GetBidItems()
    {
        var bidItems = _bidItemRepository.GetBidItems();
        return Ok(bidItems);
    }

    // GET: api/BidItems/5
    [HttpGet("{id}")]
    public IActionResult GetBidItem(int id)
    {
        var bidItem = _bidItemRepository.GetBidDetails(id);
        if (bidItem == null)
        {
            return NotFound();
        }
        return Ok(bidItem);
    }

    // POST: api/BidItems
    [HttpPost]
    public IActionResult PostBidItem([FromBody] BidItem bidItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _bidItemRepository.InsertBidItem(bidItem);
        return CreatedAtAction("GetBidItem", new { id = bidItem.id }, bidItem);
    }

    // PUT: api/BidItems/5
    [HttpPut("{id}")]
    public IActionResult PutBidItem(int id, [FromBody] BidItem bidItem)
    {
        if (id != bidItem.id)
        {
            return BadRequest();
        }

        _bidItemRepository.updateBidItem(bidItem);
        return NoContent();
    }

    // DELETE: api/BidItems/5
    [HttpDelete("{id}")]
    public IActionResult DeleteBidItem(int id)
    {
        var bidItem = _bidItemRepository.GetBidDetails(id);
        if (bidItem == null)
        {
            return NotFound();
        }

        _bidItemRepository.deleteBidItem(bidItem);
        return NoContent();
    }
}