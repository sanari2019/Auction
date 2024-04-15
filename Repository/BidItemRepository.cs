using RepoDb;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using RepoDb.DbHelpers;
using RepoDb.DbSettings;
using RepoDb.Enumerations;
using RepoDb.StatementBuilders;

public class BidItemRepository : BaseRepository<BidItem, SqlConnection>
{

    //Setting cstring=new Setting();
    Setting sett = new Setting();
    public BidItemRepository(Setting sett) : base(sett.ConString)
    {
        this.sett = sett;
        DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
        DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()), true);

    }
    public void InsertBidItem(BidItem bidItem)
{
    // Create a SQL connection using your connection string
    using (var connection = new SqlConnection(sett.ConString))
    {
        connection.Open(); // Open the connection

        // Create the SQL command with parameterized values to prevent SQL injection
        string sql = @"INSERT INTO [Auction].[dbo].[BidItem] 
                        ([itemName], [description], [takeNote], [quantity], [bidid], [active]) 
                        VALUES 
                        (@ItemName, @Description, @TakeNote, @Quantity, @BidId, @Active)";

        // Create a SQL command object with the SQL command and connection
        using (var command = new SqlCommand(sql, connection))
        {
            // Set parameter values based on the bidItem object
            command.Parameters.AddWithValue("@ItemName", bidItem.itemName);
            command.Parameters.AddWithValue("@Description", bidItem.description);
            command.Parameters.AddWithValue("@TakeNote", bidItem.takeNote);
            command.Parameters.AddWithValue("@Quantity", bidItem.quantity);
            command.Parameters.AddWithValue("@BidId", bidItem.bidid);
            command.Parameters.AddWithValue("@Active", bidItem.active);

            // Execute the SQL command to insert the data
            command.ExecuteNonQuery();
        }
    }

    // Assuming this.Insert(vehicle) is a RepoDb method call, you can remove it as the data is already inserted above
}

    public void updateBidItem(BidItem vehicle)
    {

        this.Update(vehicle);
    }
    public int deleteBidItem(BidItem vehicle)
    {

        int id = this.Delete<BidItem>(vehicle);
        return id;
    }
    public List<BidItem> GetBidItems()
    {

        var vehicless = new List<BidItem>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            var sql = "SELECT * FROM [Auction].[dbo].[bidItem]";
            vehicless = connection.ExecuteQuery<BidItem>(sql).ToList();
            /* Do the stuffs for the people here */

            // Use the Query method to load the associated BidItem objects for each BidItemImage.
            foreach (var vehicle in vehicless)
            {
                 var sql2 = "SELECT * FROM [Auction].[dbo].[Bid] WHERE id = @Id";
            // Provide parameter value using an anonymous object or a Dictionary<string, object>
            var parameters = new { Id = vehicle.bidid };
            vehicle.bid  = connection.ExecuteQuery<Bid>(sql2, parameters).FirstOrDefault();
            }
        }
        return vehicless;
    }
    public BidItem GetBidDetails(int id)
    {

        var vehicless = new BidItem();
        using (var connection = new SqlConnection(sett.ConString))
        {

            vehicless = connection.Query<BidItem>(id).FirstOrDefault();
        }
        return vehicless;
    }


}