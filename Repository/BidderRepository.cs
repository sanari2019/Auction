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

public class BidderRepository : BaseRepository<Bidder, SqlConnection>
{

    //Setting cstring=new Setting();
    Setting sett = new Setting();
    public BidderRepository(Setting sett) : base(sett.ConString)
    {
        this.sett = sett;
        DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
        DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()), true);

    }
    public void insertBidder(Bidder bida)
    {
        //UserRepository usrrepository = new UserRepository(cstring.ConString);
        bida.fLoginDate = DateTime.Now;
        bida.lLoginDate = DateTime.Now;
        this.Insert(bida);
    }
    public void updateBidder(Bidder bida)
    {

        this.Update(bida);
    }
    public int deleteBidder(Bidder bida)
    {

        int id = this.Delete<Bidder>(bida);
        return id;
    }
    public List<Bidder> GetBidders()
    {

        var bidders = new List<Bidder>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            bidders = connection.QueryAll<Bidder>().ToList();
            /* Do the stuffs for the people here */
        }
        return bidders;
    }
    public Bidder GetBidder(int id)
    {

        var bidder = new Bidder();
        using (var connection = new SqlConnection(sett.ConString))
        {

            bidder = connection.Query<Bidder>(id).FirstOrDefault();
        }
        return bidder;
    }
    public Bidder GetBidderByUsername(string username)
    {

        var bidder = new Bidder();
        using (var connection = new SqlConnection(sett.ConString))
        {
            bidder = connection.Query<Bidder>(e => e.username == username).FirstOrDefault();
        }
        return bidder;
    }

    public void UpdateBidder(Bidder bidder)
    {
        using (var connection = new SqlConnection(sett.ConString))
        {
            // Ensure that the 'bidder' object has a valid 'id' property
            if (bidder.id <= 0)
            {
                throw new ArgumentException("Invalid 'id' value for updating Bidder.");
            }

            // Use RepoDb's Update method to update the Bidder record
            var rowsUpdated = connection.Update<Bidder>(bidder);

            // Check the number of rows updated
            if (rowsUpdated <= 0)
            {
                throw new InvalidOperationException("No Bidder record was updated.");
            }
        }
    }


}