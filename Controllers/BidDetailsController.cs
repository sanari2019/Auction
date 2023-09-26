using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController]
[Route("[controller]")]
public class BidDetailsController : ControllerBase
{
    BidDetailsRepository _repository;
    private BidderRepository repuser;
    int idvalue = 0;
    private EmailConfiguration _emailConfig;
    public BidDetailsController(BidDetailsRepository repository, EmailConfiguration emailConfig, BidderRepository repUser)
    {
        _repository = repository;
        this._emailConfig = emailConfig;
        this.repuser = repUser;
    }



    // GET: api/BidDetails
    [HttpGet]
    public async Task<ActionResult> GetBidDetails()
    {
        var bidDetails = _repository.GetBidDetails();
        return Ok(bidDetails);
    }

    // GET: api/BidDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetBidDetails(int id)
    {
        var bidDetails = _repository.GetBidDetails(id);
        if (bidDetails == null)
        {
            return NotFound();
        }
        return Ok(bidDetails);
    }

    [HttpGet("getbiddetails/{vehicleId}")]
    public async Task<ActionResult> GetBidDetailsByVehicle(int vehicleId)
    {
        var bidDetails = _repository.GetBidDetailsByVehicle(vehicleId);
        if (bidDetails == null)
        {
            return NotFound();
        }
        return Ok(bidDetails);
    }
    [HttpGet("getbiddetail/{bidderId}")]
    public async Task<ActionResult> GetBidDetailsByBidder(int bidderId)
    {
        var bidDetails = _repository.GetBidDetailsByBidder(bidderId);

        return Ok(bidDetails);
    }

    [HttpGet("latestBid")]
    public IActionResult GetLatestBid(int staffId, int vehicleId)
    {
        // Call your data access method to retrieve the latest bid
        var latestBid = _repository.GetLatestBidByBidderAndVehicle(staffId, vehicleId);

        if (latestBid != null)
        {
            // If a bid is found, return it as a 200 OK response
            return Ok(latestBid);
        }
        else
        {
            // If no bid is found, return a 404 Not Found response
            return NotFound();
        }
    }

    // POST: api/BidDetails
    [HttpPost]
    public async Task<ActionResult<BidDetails>> PostBidDetails([FromBody] BidDetails bidDetails)
    {
        if (bidDetails == null)
        {
            return BadRequest();
        }
        if (bidDetails != null)
        {
            Bidder us = new Bidder();
            us = repuser.GetBidder(bidDetails.bidderId);
            EmailSender _emailSender = new EmailSender(this._emailConfig);
            Email em = new Email();
            string logourl = "";//"https://evercaregroup.com/wp-content/uploads/2020/12/EVERCARE_LOGO_03_LEKKI_PRI_FC_RGB.png";
            string applink = "";
            string salutation = "Dear " + us.fname + ",";
            string emailcontent = "You have successfully placed a bid of: " + "NGN" + bidDetails.staffBid.ToString("N") + " for" + bidDetails.Vehicle.name;
            string narration1 = " ";
            string econtent = em.HtmlMail("Bid Confirmation", applink, salutation, emailcontent, narration1, logourl);
            var recipientEmail = us.username + "@evercare.ng";
            var message = new Message(new string[] { recipientEmail }, "Bidding Application", econtent);
            await _emailSender.SendEmailAsync(message);
            _repository.insertBidDetails(bidDetails);
        }
        return Ok(bidDetails);


    }

    // PUT: api/BidDetails/5
    [HttpPut("{id}")]
    public IActionResult PutBidDetails(int id, [FromBody] BidDetails bidDetails)
    {
        if (id != bidDetails.id)
        {
            return BadRequest();
        }
        _repository.updateBiddetails(bidDetails);
        return NoContent();
    }

    // DELETE: api/BidDetails/5
    [HttpDelete("{id}")]
    public IActionResult DeleteBidDetails(int id)
    {
        var bidDetails = _repository.GetBidDetails(id);
        if (bidDetails == null)
        {
            return NotFound();
        }
        _repository.deleteBidDetails(bidDetails);
        return NoContent();
    }

    // Define your CRUD actions here
}