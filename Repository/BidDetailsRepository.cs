using RepoDb;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using RepoDb.DbHelpers;
using RepoDb.DbSettings;
using RepoDb.Enumerations;
using RepoDb.StatementBuilders;

public class BidDetailsRepository : BaseRepository<BidDetails, SqlConnection>
{

    //Setting cstring=new Setting();
    Setting sett = new Setting();
    public BidDetailsRepository(Setting sett) : base(sett.ConString)
    {
        this.sett = sett;
        DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
        DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()), true);

    }
    public void insertBidDetails(BidDetails biddetails)
    {
        //UserRepository usrrepository = new UserRepository(cstring.ConString);
         // Create a SQL connection using your connection string
    using (var connection = new SqlConnection(sett.ConString))
    {
        connection.Open(); // Open the connection

        // Create the SQL command with parameterized values to prevent SQL injection
        var sql = @"INSERT INTO [Auction].[dbo].[BidDetails] 
                        ([bidid], [vehicleid], [itemid], [staffid], [staffbid], [biddate]) 
                        VALUES 
                        (@bidid, @vehicleid, @itemid, @staffid, @staffbid, @biddate)";

        // Create a SQL command object with the SQL command and connection
        using (var command = new SqlCommand(sql, connection))
        {
            // Set parameter values based on the bidItem object
            command.Parameters.AddWithValue("@bidid", biddetails.bidId);
            command.Parameters.AddWithValue("@vehicleid", biddetails.vehicleId);
            command.Parameters.AddWithValue("@itemid", biddetails.itemId);
            command.Parameters.AddWithValue("@staffid", biddetails.staffId);
            command.Parameters.AddWithValue("@staffbid", biddetails.staffBid);
            command.Parameters.AddWithValue("@biddate", biddetails.bidDate);

            // Execute the SQL command to insert the data
            command.ExecuteNonQuery();
        }
    }
        // this.Insert(biddetails);
    }
    public void updateBiddetails(BidDetails biddetails)
    {

        this.Update(biddetails);
    }
    public int deleteBidDetails(BidDetails biddetails)
    {

        int id = this.Delete<BidDetails>(biddetails);
        return id;
    }
    public List<BidDetails> GetBidDetails()
    {

        var biddetailss = new List<BidDetails>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            biddetailss = connection.QueryAll<BidDetails>().ToList();
            /* Do the stuffs for the people here */
        }
        return biddetailss;
    }
    public BidDetails GetBidDetails(int id)
    {

        var biddetails = new BidDetails();
        using (var connection = new SqlConnection(sett.ConString))
        {

            biddetails = connection.Query<BidDetails>(id).FirstOrDefault();
        }
        return biddetails;
    }

    public List<BidDetails> GetBidDetailsByVehicle(int vehicleId)
    {

        var biddetailss = new List<BidDetails>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            biddetailss = connection.ExecuteQuery<BidDetails>("SELECT * FROM [dbo].[Biddetails] B WHERE B.vehicleid= @vehicleId", new { vehicleid = vehicleId }).ToList();
            /* Do the stuffs for the people here */
        }
        return biddetailss;
    }
     public List<BidDetails> GetBidDetailsByItem(int itemId)
    {

        var biddetailss = new List<BidDetails>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            biddetailss = connection.ExecuteQuery<BidDetails>("SELECT * FROM [Auction].[dbo].[Biddetails] B WHERE B.itemId= @itemId", new { itemId = itemId }).ToList();
            /* Do the stuffs for the people here */
        }
        return biddetailss;
    }


    public BidDetails GetBidDetailsByBidder(int staffId)
    {

        var biddetailss = new BidDetails();
        using (var connection = new SqlConnection(sett.ConString))
        {
            biddetailss = connection.ExecuteQuery<BidDetails>("SELECT * FROM [dbo].[Biddetails] B WHERE B.staffid= @staffId ORDER BY B.bidDate DESC", new { staffid = staffId }).FirstOrDefault();
            /* Do the stuffs for the people here */
        }
        return biddetailss;
    }

    public BidDetails? GetLatestBidByBidderAndVehicle(int staffId, int vehicleId)
    {
        BidDetails? latestBid = null;

        using (var connection = new SqlConnection(sett.ConString))
        {
            // Adjust the SQL query to select the most recent bid for a specific staffId and vehicleId based on 'id'
            var query = "SELECT TOP 1 B.* FROM [Auction].[dbo].[Biddetails] B WHERE B.staffid = @staffId AND B.vehicleid = @vehicleId ORDER BY B.id DESC";

            // Use Dapper to execute the query and retrieve the first (most recent) result
            var result = connection.ExecuteQuery<BidDetails>(query, new { staffId, vehicleId });
            latestBid = result.FirstOrDefault();
        }

        return latestBid;
    }

    public BidDetails? GetLatestBeedByBidderAndVehicle(int staffId, int itemId)
    {
        BidDetails? latestBid = null;

        using (var connection = new SqlConnection(sett.ConString))
        {
            // Adjust the SQL query to select the most recent bid for a specific staffId and vehicleId based on 'id'
            var query = "SELECT TOP 1 B.* FROM [Auction].[dbo].[Biddetails] B WHERE B.staffId = @staffId AND B.itemId = @itemId ORDER BY B.id DESC";
            var parameter = new { staffId = @staffId, itemId=@itemId};
            // Use Dapper to execute the query and retrieve the first (most recent) result
            var result = connection.ExecuteQuery<BidDetails>(query, parameter).FirstOrDefault();
            latestBid = result;
        }

        return latestBid;
    }








}