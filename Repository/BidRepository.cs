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

public class BidRepository: BaseRepository<Bid, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public BidRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertBid(Bid bid)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(bid);
        }
        public  void updateBid(Bid bid)
        {
           
            this.Update(bid);
        }
        public int deleteBid(Bid bid)
        {
           
            int id = this.Delete<Bid>(bid);
            return id;
        }
        public List<Bid> GetBids()
        {  
          
           var bids= new List<Bid>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                bids = connection.QueryAll<Bid>().ToList();
                /* Do the stuffs for the people here */
            }
          return bids;
        }
        public Bid GetBid(int id)
        {  
          
           var bid= new Bid();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               bid = connection.Query<Bid>(id).FirstOrDefault();
            }
          return bid;
        }
        // public Bid GetBidder(string username)
        // {  
          
        //    var bidder= new Bidder();
        //    using (var connection = new SqlConnection(sett.ConString))
        //     {
        //        bidder = connection.Query<Bidder>(e=>e.username==username).FirstOrDefault();
        //     }
        //   return bidder;
        // }
         
    }