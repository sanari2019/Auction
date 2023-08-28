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

public class BidDetailsRepository: BaseRepository<BidDetails, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public BidDetailsRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertBidDetails(BidDetails biddetails)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(biddetails);
        }
        public  void updateBiddetails(BidDetails biddetails)
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
          
           var biddetailss= new List<BidDetails>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                biddetailss = connection.QueryAll<BidDetails>().ToList();
                /* Do the stuffs for the people here */
            }
          return biddetailss;
        }
        public BidDetails GetBidDetails(int id)
        {  
          
           var biddetails= new BidDetails();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               biddetails = connection.Query<BidDetails>(id).FirstOrDefault();
            }
          return biddetails;
        }
        
         
    }