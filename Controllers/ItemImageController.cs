using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net;

// using iText.Kernel.Pdf;
// using iText.Layout;
// // using iText.Html2pdf;
// using iText.Layout.Element;
// using iText.Layout.Properties;



    [Route("[controller]")]
    [ApiController]
    public class ItemImageController : ControllerBase
    {
        private readonly ItemImageRepository _doctorSignatureRepository;

        public ItemImageController(ItemImageRepository doctorSignatureRepository)
        {
            _doctorSignatureRepository = doctorSignatureRepository;
        }

         [HttpGet]
    public async Task<ActionResult> GetItemImages()
    {
        return new OkObjectResult(_doctorSignatureRepository.GetItemImages());
    }
    // GET: api/Cities/5
    [HttpGet("{vehicleId}")]
    public async Task<ActionResult<ItemImage>> GetImagesbyVehicleId(int vehicleId)
    {
        return new OkObjectResult(_doctorSignatureRepository.GetImagesbyItemId(vehicleId));

    }
    // GET: api/ItemImage/default
    [HttpGet("default")]
    public IEnumerable<ItemImage> GetDefaultImages()
    {
        return _doctorSignatureRepository.GetDefaultItemImages();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutItemImage(int id, ItemImage vehicleImage)
    {
        if (id != vehicleImage.itemId)
        {
            return BadRequest();
        }
        // _context.Entry(state).State = EntityState.Modified;

        _doctorSignatureRepository.updateItemImage(vehicleImage);
        return new OkObjectResult(vehicleImage);

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
    public async Task<ActionResult<Bid>> PostItemImage(ItemImage vehicleImage)
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
        if (vehicleImage != null)
        {
            _doctorSignatureRepository.insertItemImage(vehicleImage);
        }
        return Ok(vehicleImage);

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
    // [HttpPost("deletevehicleImage")]
    // public async Task<IActionResult> DeleteBid([FromBody] ItemImage vehicleimage)
    // {

    //     idvalue = _doctorSignatureRepository.deleteItemImage(vehicleimage);
    //     return Ok(vehicleimage);
    // }



        // [HttpPost("convert-to-docx")]
        // public IActionResult ConvertToDocx(string htmlContent)
        // {
        //     // Convert HTML content to DOCX format using HtmlToOpenXml library
        //     var htmlToDocxConverter = new HtmlConverter();
        //     var docxBytes = htmlToDocxConverter.ParseHtml(htmlContent, Encoding.UTF8);

        //     // Save the converted DOCX content to a file
        //     var fileName = $"{Guid.NewGuid()}.docx";
        //     var filePath = Path.Combine("wwwroot/files/", fileName);
        //     System.IO.File.WriteAllBytes(filePath, docxBytes);

        //     // Save file path to the database
        //     var filePathInDatabase = $"/files/{fileName}";
        //     var resultFile = new ResultFiles { filePath = filePathInDatabase };
        //     _resultFilesRepository.AddFileAsync(resultFile);

        //     return Ok(new { filePath = filePathInDatabase });
        // }

        // [HttpPost("convert-to-pdf")]
        // public IActionResult ConvertToPdf(string htmlContent)
        // {
        //     // Create a new PDF document
        //     var pdfFileName = $"{Guid.NewGuid()}.pdf";
        //     var pdfFilePath = Path.Combine("wwwroot/files/", pdfFileName);

        //     using (var pdfWriter = new PdfWriter(pdfFilePath))
        //     {
        //         using (var pdfDocument = new Document(pdfWriter))
        //         {
        //             // Convert HTML content to PDF and add it to the document
        //             HtmlConverter.ConvertToPdf(htmlContent, pdfDocument, pdfWriter);
        //         }
        //     }

        //     // Save file path to the database
        //     var filePathInDatabase = $"/files/{pdfFileName}";
        //     var resultFile = new ResultFiles { filePath = filePathInDatabase };
        //     _resultFilesRepository.AddFileAsync(resultFile);

        //     return Ok(new { filePath = filePathInDatabase });
        // }

// [HttpPost("convert-to-pdf")]
//         public IActionResult ConvertToPdf(string htmlContent)
//         {
//             try
//             {
//                 // Generate unique file name for PDF
//                 var pdfFileName = $"{Guid.NewGuid()}.pdf";
//                 var pdfFilePath = Path.Combine("wwwroot/files/", pdfFileName);

//                 // Convert HTML content to PDF and save to file
//                 // Convert HTML content to PDF and save to file
//                 using (FileStream pdfFile = new FileStream(pdfFilePath, FileMode.Create))
//                 {
//                     HtmlConverter.ConvertToPdf(htmlContent, pdfFile);
//                 }

//                 // Save file path to the database
//                 var filePathInDatabase = $"/files/{pdfFileName}";
//                 var resultFile = new ResultFiles { filePath = filePathInDatabase };
//                 _resultFilesRepository.AddFileAsync(resultFile);

//                 return Ok(new { filePath = filePathInDatabase });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Error converting HTML to PDF: {ex.Message}");
//             }
//         }


        // [HttpPost("convert-to-pdf")]
        // public IActionResult ConvertToPdf(string htmlContent)
        // {
        //     // Create a new PDF document
        //     var pdfFileName = $"{Guid.NewGuid()}.pdf";
        //     var pdfFilePath = Path.Combine("wwwroot/files/", pdfFileName);

        //     // Convert HTML content to PDF and save to file
        //     // PdfDocument pdfDocument = PdfGenerator.GeneratePdf(htmlContent, PdfSharp.PageSize.A4);
        //     // pdfDocument.Save(pdfFilePath);

        //     // Save file path to the database
        //     var filePathInDatabase = $"/files/{pdfFileName}";
        //     var resultFile = new ResultFiles { filePath = filePathInDatabase };
        //     _resultFilesRepository.AddFileAsync(resultFile);

        //     return Ok(new { filePath = filePathInDatabase });
        // }

        [HttpPost("upload")]
public IActionResult UploadFiles([FromForm] List<Microsoft.AspNetCore.Http.IFormFile> files)
{
    if (files != null && files.Count > 0)
    {
        List<string> fileUrls = new List<string>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                // Generate unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var imageUrl = Path.Combine("wwwroot/files/", fileName); // Save files in wwwroot/files folder

                // Save file to server
                using (var stream = new FileStream(imageUrl, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Save file file path to list
                var filePathInDatabase = $"/files/{fileName}"; // Assuming 'files' is the virtual path to your files folder
                fileUrls.Add(filePathInDatabase);
            }
        }

        // Save file URLs to the database
        foreach (var fileUrl in fileUrls)
        {
            var resultFiles = new ItemImage { imageUrl = fileUrl };
            _doctorSignatureRepository.AddFileAsync(resultFiles);
        }

        return Ok(new { fileUrls });
    }
    else
    {
        return BadRequest("No files uploaded or invalid files");
    }
}




        [HttpPost("upload")]
        public IActionResult UploadFile([FromForm] Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Generate unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var imageUrl = Path.Combine("wwwroot/files/", fileName); // Save files in wwwroot/files folder

                // Save file to server
                using (var stream = new FileStream(imageUrl, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Save file file path to database
                var filePathInDatabase = $"/files/{fileName}"; // Assuming 'files' is the virtual path to your files folder
                var resultFiles = new ItemImage { imageUrl = filePathInDatabase };
                _doctorSignatureRepository.AddFileAsync(resultFiles);

                return Ok(new { imageUrl = filePathInDatabase });
            }
            else
            {
                return BadRequest("No file uploaded or invalid file");
            }
        }

        // [HttpGet("{id}/file")]
        // public IActionResult GetFile(int id)
        // {
        //     var resultFile = _doctorSignatureRepository.GetById(id);
        //     if (resultFile == null)
        //     {
        //         return NotFound();
        //     }

        //     // Construct the URL for accessing the file in the frontend
        //     var fileUrl = $"{Request.Scheme}://{Request.Host}/api/resultfiles/file/{id}";

        //     return Ok(new { fileUrl });
        // }

        // [HttpGet("file/{id}")]
        // public IActionResult GetResuleFile(int id)
        // {
        //     var resultFile = _resultFilesRepository.GetById(id);
        //     if (resultFile == null)
        //     {
        //         return NotFound();
        //     }

        //     // Construct the file path based on the stored file path
        //     var filePath = Path.Combine("wwwroot/files/", resultFile.filePath);

        //     // Read the file file and return it as a file result
        //     var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //     return File(fileBytes, "file/docx"); // Adjust content type based on your file type
        // }
        [HttpGet("file/{employeeId}")]
        public IActionResult GetItemImagesByEmployeeId(int employeeId)
        {
            var doctorSignatures = _doctorSignatureRepository.GetByItemId(employeeId);
            if (doctorSignatures == null)
            {
                return NotFound("No doctor signatures found for the given employee ID.");
            }
             try
        {
            // Append wwwroot to the imageUrl
        string imagePath = "wwwroot"+ doctorSignatures.imageUrl;

        // Check if the file exists
        if (!System.IO.File.Exists(imagePath))
        {
            return NotFound("Signature image file not found.");
        }

        // Read the image file into a byte array
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        
        // Convert the byte array to base64 string
        string base64String = Convert.ToBase64String(imageBytes);
        
        // Return the base64 string as part of the response
        return Ok(new { signatureBase64 = base64String });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error occurred while converting image to base64: {ex.Message}");
        }

            // return Ok(new { imageUrl = doctorSignatures.imageUrl });
            // return;
        }


        //  [HttpGet("file/{id}")]
        // public IActionResult GetResultFile(int id)
        // {
        //     var resultFile = _doctorSignatureRepository.GetById(id);
        //     if (resultFile == null)
        //     {
        //         return NotFound();
        //     }

        //     // Construct the file path based on the stored file path
        //     var imageUrl = Path.Combine("wwwroot/files/", resultFile.imageUrl);

        //     // Check if the file is a DOCX file
        //     if (Path.GetExtension(imageUrl).Equals(".docx", StringComparison.OrdinalIgnoreCase))
        //     {
        //         // Read the existing DOCX file
        //         using (var existingDoc = WordprocessingDocument.Open(imageUrl, false))
        //         {
        //             var newFilePath = Path.Combine("wwwroot/files/", $"{Guid.NewGuid()}.docx");
        //             using (var newDoc = WordprocessingDocument.Create(newFilePath, WordprocessingDocumentType.Document))
        //             {
        //                 // Copy the content of the existing DOCX file to the new file
        //                 foreach (var part in existingDoc.Parts)
        //                 {
        //                     newDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
        //                 }
        //             }

        //             // Save the new file path to the database
        //             var newFilePathInDatabase = $"/files/{Path.GetFileName(newFilePath)}";
        //             var newResultFile = new ItemImage { imageUrl = newFilePathInDatabase };
        //             _doctorSignatureRepository.AddFileAsync(newResultFile);

        //             // Return the new file for download
        //             var fileBytes = System.IO.File.ReadAllBytes(newFilePath);
        //             return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "new_file.docx");
        //         }
        //     }
        //     else
        //     {
        //         // Handle if the file is not a DOCX file
        //         return BadRequest("File is not a DOCX file.");
        //     }
        // }

    }


