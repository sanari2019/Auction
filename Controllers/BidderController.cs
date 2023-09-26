using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;




[ApiController]
[Route("[controller]")]
public class BidderController : ControllerBase
{

    BidderRepository _repBidder;
    private readonly IConfiguration _configuration;
    int idvalue = 0;
    public BidderController(BidderRepository repBidder, IConfiguration configuration)
    {
        this._repBidder = repBidder;
        this._configuration = configuration;
        // this._emailConfig = emailConfig;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetBidders()
    {
        return new OkObjectResult(_repBidder.GetBidders());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bidder>> GetBidder(int id)
    {
        return new OkObjectResult(_repBidder.GetBidder(id));

    }

    [HttpGet("getuser/{userName}")]
    public async Task<ActionResult<Bidder>> GetBidderByUsername(string username)
    {
        return new OkObjectResult(_repBidder.GetBidderByUsername(username));
    }
    // PUT: api/users/5
    // To protect from overposting attacks, see https://go.microsoft.com/
    // fwlink/?linkid=2123754

    //return NoContent();
    // POST: api/Cities
    // To protect from overposting attacks, see https://go.microsoft.com/
    //fwlink /? linkid = 2123754
    // [HttpPost]
    // public async Task<ActionResult<Bidder>> PostUser(Bidder bidder)
    // {

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
    // if (user != null)
    // {
    //     repuser.insertUser(user);
    // }
    // return Ok(user);

    // }
    [HttpPost("login")]
    public IActionResult Login([FromBody] Bidder loginUser)
    {
        Bidder us = new Bidder();
        if (loginUser == null)
        {
            return BadRequest("Invalid client request");
        }
        else
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "evercare.ng"))
            {
                Directory dir = new Directory(_repBidder);
                string IVkey = "0123456789abcdef|0123456789abcdef";
                string decryptedpassword = dir.NgDecrypt(loginUser.password, IVkey);
                bool isValid = pc.ValidateCredentials(loginUser.username, decryptedpassword);
                if (isValid == true)
                {

                    bool testAvail = dir.IsExistInAD(loginUser);
                    if (testAvail == true)
                    {
                        List<Bidder> bidders = new List<Bidder>();
                        bidders = _repBidder.GetBidders();
                        foreach (Bidder bidder in bidders)
                        {
                            if (bidder.username.ToLower() == loginUser.username.ToLower())
                            {
                                us = bidder;

                            }

                        }
                        if (us != null)
                        {
                            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));
                            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                            var tokeOptions = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: signinCredentials
                            );
                            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                            return Ok(new AuthenticatedResponse { Token = tokenString });
                        }
                    }


                }

            }
        }

        return Unauthorized();

    }
    // PUT: api/Bidder/5
    [HttpPut("{id}")]
    public IActionResult UpdateBidder(int id, [FromBody] Bidder bidder)
    {
        if (bidder == null || id != bidder.id)
        {
            return BadRequest();
        }

        try
        {
            // Attempt to update the Bidder record in the repository
            _repBidder.UpdateBidder(bidder);
            return NoContent(); // Indicates successful update (204 No Content)
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the update process
            // Log the exception or return an appropriate error response
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    // DELETE: api/Cities/5
    [HttpPost("deleteuser")]
    public async Task<IActionResult> Delete([FromBody] Bidder bidder)
    {

        // if (idvalue != us.id)
        //  {
        //  return NotFound();
        //  }
        idvalue = _repBidder.deleteBidder(bidder);
        return Ok(bidder);
    }
    //private bool StateExists(int id)
    // {
    // return _context.States.Any(e => e.Id == id);
    // }

}