using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Cryptography;
using System.Text;

public class Directory
{
    public BidderRepository _repBidder;
    public Directory(BidderRepository repBidder)
    {
        this._repBidder = repBidder;
    }
    public bool IsExistInAD(Bidder loginUser)
    {
        Bidder bidder = new Bidder();
        string userName = loginUser.username;
        DirectorySearcher search = new DirectorySearcher();
        search.Filter = String.Format("(SAMAccountName={0})", loginUser.username);
        search.PropertiesToLoad.Add("givenname");
        search.PropertiesToLoad.Add("sn");
        search.PropertiesToLoad.Add("cn");
        search.PropertiesToLoad.Add("Department");
        SearchResult result = search.FindOne();
        if (result != null)
        {
            bidder.username = loginUser.username.ToLower();
            bidder.fname = (string)result.Properties["givenname"][0];
            bidder.lname = (string)result.Properties["sn"][0];
            string cname = (string)result.Properties["cn"][0];
            // log.department = (string)result.Properties["Department"][0];
            List<Bidder> bidders = new List<Bidder>();
            bidders = _repBidder.GetBidders();
            Bidder loginBidder = new Bidder();
            bool testavail = false;
            foreach (Bidder bidderdet in bidders)
            {
                if (bidderdet.username == bidder.username)
                {
                    testavail = true;
                    loginBidder = bidderdet;
                    loginBidder.id = bidderdet.id;
                    loginBidder.lLoginDate = DateTime.Now;
                    loginBidder.fname = bidder.fname;
                    loginBidder.lname = bidder.lname;
                    loginBidder.password = loginUser.password;
                    _repBidder.updateBidder(bidderdet);
                    // Session.Add("user", user);
                    // Response.Redirect("Default.aspx");
                }

            }
            if (testavail == false)
            {
                // Session.Add("user", log);
                loginBidder.fLoginDate = DateTime.Now;
                loginBidder.lLoginDate = DateTime.Now;
                loginBidder.fname = bidder.fname;
                loginBidder.lname = bidder.lname;
                loginBidder.password = loginUser.password;
                loginBidder.username = bidder.username;
                _repBidder.insertBidder(loginBidder);

                // Response.Redirect("Default.aspx");
            }
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }

    public string NgDecrypt(string encryptedText, string ngCipherKeyIvPhrase)
    {
        string plainText = string.Empty;
        var cipherTextArray = ngCipherKeyIvPhrase.Split("|");
        string cipherPhrase = cipherTextArray[0];
        string salt = cipherTextArray[1];

        byte[] cipherText = Convert.FromBase64String(encryptedText);
        // Create an RijndaelManaged object  
        // with the specified key and IV.  
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings  
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = Encoding.UTF8.GetBytes(cipherPhrase);
            rijAlg.IV = Encoding.UTF8.GetBytes(salt);

            // Create a decryptor to perform the stream transform.  
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.  
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {

                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream  
                        // and place them in a string.  
                        plainText = srDecrypt.ReadToEnd();

                    }

                }
            }

            return plainText;

        }

    }
}