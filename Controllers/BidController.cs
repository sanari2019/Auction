using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class BidController : ControllerBase
{

    BidRepository _repbid;
    int idvalue=0;
   
    public BidController(BidRepository _repbid)
    {
        this._repbid = _repbid;
        // this._emailConfig = emailConfig;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetBids()
    {
        return new OkObjectResult(_repbid.GetBids());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bid>> GetBid(int id)
    {
        return new OkObjectResult(_repbid.GetBid(id));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutBid(int id, Bid bid)
    {
        if (id != bid.id)
        {
            return BadRequest();
        }
        // _context.Entry(state).State = EntityState.Modified;

        _repbid.updateBid(bid);
        return new OkObjectResult(bid);

        // catch (DbUpdateConcurrencyException)
        // {
        // if (!StateExists(id))
        //  {
        //  return NotFound();
        // }
        // else
        //  {
        // throw;
        // }
    }
    //return NoContent();
    // POST: api/Cities
    // To protect from overposting attacks, see https://go.microsoft.com/
    //fwlink /? linkid = 2123754
    [HttpPost]
    public async Task<ActionResult<Bid>> PostBid(Bid bid)
    {

        // EmailSender _emailSender = new EmailSender(this._emailConfig);
        // Email em = new Email();
        // string logourl = ""; //"https://evercaregroup.com/wp-content/uploads/2020/12/EVERCARE_LOGO_03_LEKKI_PRI_FC_RGB.png";
        // string applink = "https://cafeteria.evercare.ng";
        // string salutation = "Dear " + user.firstName + ",";
        // string emailcontent = "Your account has been successfully created on Evercare's Food & Beverage Application. You can now enjoy a seamless dining experience with our app.";
        // string narration1 = "Thank you for choosing Evercare's Cafeteria!";
        // string econtent = em.HtmlMail("Welcome to Evercare's Cafeteria", applink, salutation, emailcontent, narration1, logourl);
        // var message = new Message(new string[] { user.userName }, "Cafeteria Application", econtent);
        // //  _emailSender.SendEmail(message);
        // await _emailSender.SendEmailAsync(message);
        if (bid != null)
        {
            _repbid.insertBid(bid);
        }
        return Ok(bid);

    }
    // [HttpPost("login")]
    // public IActionResult Login([FromBody] User loginUser)
    // {
    //     // if (loginUser is null)
    //     // {
    //     //     return BadRequest("Invalid client request");
    //     // }
    //     User us=new User();
    //     us=repuser.GetUser(loginUser.userName);
    //     if (us !=null)
    //     {
    //         var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("superSecretKey@345"));
    //         var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    //         var tokeOptions = new JwtSecurityToken(
    //             issuer: "https://localhost:7146",
    //             audience: "https://localhost:7146",
    //             // claims: new List<Claim>(),
    //             expires: DateTime.Now.AddMinutes(10),
    //             signingCredentials: signinCredentials
    //         );
    //         var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    //         return Ok(new AuthenticatedResponse { Token = tokenString });
    //     }
    //     return Unauthorized();
    // }
    // DELETE: api/Cities/5
    [HttpPost("deletebid")]
    public async Task<IActionResult> DeleteBid([FromBody] Bid bid)
    {

        idvalue = _repbid.deleteBid(bid);
        return Ok(bid);
    }
   

}