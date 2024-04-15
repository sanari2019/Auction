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

public class ItemImageRepository : BaseRepository<ItemImage, SqlConnection>
{

    //Setting cstring=new Setting();
    Setting sett = new Setting();
    public ItemImageRepository(Setting sett) : base(sett.ConString)
    {
        this.sett = sett;
        DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
        DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()), true);

    }
    public void insertItemImage(ItemImage vehicleimage)
    {
        //UserRepository usrrepository = new UserRepository(cstring.ConString);
        this.Insert(vehicleimage);
    }
    public void updateItemImage(ItemImage vehicleimage)
    {
        this.Update(vehicleimage);
    }
    public int deleteItemImage(ItemImage vehicleimage)
    {

        int id = this.Delete<ItemImage>(vehicleimage);
        return id;
    }
    public List<ItemImage> GetItemImages()
    {

        var vehicleImages = new List<ItemImage>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            vehicleImages = connection.QueryAll<ItemImage>().ToList();
            foreach (ItemImage vehicleimage in vehicleImages)
            {
                vehicleimage.bidItem = connection.Query<BidItem>(vehicleimage.itemId).FirstOrDefault();
                if (vehicleimage.bidItem != null)
                {
                    vehicleimage.bidItem.bid = connection.Query<Bid>(vehicleimage.bidItem.bidid).FirstOrDefault();
                }
            }
        }
        return vehicleImages;
    }
    public List<ItemImage> GetImagesbyItemId(int itemId)
    {

        List<ItemImage> vehicleImages = new List<ItemImage>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            var sql = "SELECT * FROM [Auction].[dbo].[ItemImage] WHERE itemId = @itemId";
            vehicleImages = connection.ExecuteQuery<ItemImage>(sql, new { itemId }).ToList();

            // vehicleImages = connection.QueryAll<ItemImage>().Where(vi => vi.itemId == itemId).ToList();
            // Use the Query method to load the associated Item objects for each ItemImage.
            foreach (var vehicleImage in vehicleImages)
            {
                var sql2 = "SELECT TOP 1 * FROM [Auction].[dbo].[BidItem] WHERE Id = @Id";
                vehicleImage.bidItem = connection.ExecuteQuery<BidItem>(sql2, new { Id = vehicleImage.itemId }).FirstOrDefault();
                // vehicleImage.bidItem = connection.Query<BidItem>(vehicleImage.itemId).FirstOrDefault();

                if (vehicleImage.bidItem != null)
                {
                    var sql3 = "SELECT TOP 1 * FROM [Auction].[dbo].[Bid] WHERE id = @id";
                    vehicleImage.bidItem.bid = connection.ExecuteQuery<Bid>(sql3, new { id = vehicleImage.bidItem.bidid }).FirstOrDefault();
                    // vehicleImage.bidItem.bid = connection.Query<Bid>(vehicleImage.bidItem.bidid).FirstOrDefault();
                }
            }
        }
        return vehicleImages;
    }

    public List<ItemImage> GetDefaultItemImages()
    {
        var vehicleImages = new List<ItemImage>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            var sql = "SELECT * FROM [Auction].[dbo].[ItemImage] WHERE defaultImage = 1";
            vehicleImages = connection.ExecuteQuery<ItemImage>(sql).ToList();
            // vehicleImages = connection.Query<ItemImage>(new { defaultImage = true }).ToList();
            foreach (ItemImage vehicleimage in vehicleImages)
            {
                var sql2 = "SELECT * FROM [Auction].[dbo].[BidItem] WHERE id = @Id";
            // Provide parameter value using an anonymous object or a Dictionary<string, object>
                var parameters = new { Id = vehicleimage.itemId };
                // vehicleimage.vehicle = connection.ExecuteQuery<Vehicle>(sql2, parameters).FirstOrDefault();
                vehicleimage.bidItem = connection.ExecuteQuery<BidItem>(sql2, parameters).FirstOrDefault();
                if (vehicleimage.bidItem != null)
                {
                    var sql3 = "SELECT * FROM [Auction].[dbo].[Bid] WHERE id = @Id";
                    var parameters2 = new { Id = vehicleimage.bidItem.bidid };
                    vehicleimage.bidItem.bid = connection.ExecuteQuery<Bid>(sql3, parameters2).FirstOrDefault();
                    // vehicleimage.bidItem.bid = connection.Query<Bid>(vehicleimage.bidItem.bidid).FirstOrDefault();
                }
            }
            // Use RepoDb's Query method to filter by defaultImage = true
            return vehicleImages;
        }
    }

    public void AddFileAsync(ItemImage doctorSignature)
        {
            using (var connection = new SqlConnection(sett.ConString))
            {
                connection.Insert(doctorSignature);

            }
        }
//         public ItemImage GetById(int id)
//         {
//             using (var connection = new SqlConnection(sett.ConString))
//             {
// #pragma warning disable CS8603 // Possible null reference return.
//                 return connection.Query<ResultFiles>(where: x => x.id == id).FirstOrDefault();
// #pragma warning restore CS8603 // Possible null reference return.
//             }
//         }
    public ItemImage GetByItemId(int itemId)
    {
        using (var connection = new SqlConnection(sett.ConString))
        {
            var query = "SELECT * FROM ItemImage WHERE itemId = @itemId";
            return connection.ExecuteQuery<ItemImage>(query, new { itemId = itemId }).FirstOrDefault();
        }
    }

     public List<ItemImage> GetsByItemId(int itemId)
    {
        using (var connection = new SqlConnection(sett.ConString))
        {
            var query = "SELECT * FROM ItemImage WHERE itemId = @itemId";
            return connection.ExecuteQuery<ItemImage>(query, new { itemId = itemId }).ToList();
        }
    }

}