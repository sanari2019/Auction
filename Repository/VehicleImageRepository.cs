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

public class VehicleImageRepository : BaseRepository<VehicleImage, SqlConnection>
{

    //Setting cstring=new Setting();
    Setting sett = new Setting();
    public VehicleImageRepository(Setting sett) : base(sett.ConString)
    {
        this.sett = sett;
        DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
        DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()), true);

    }
    public void insertVehicleImage(VehicleImage vehicleimage)
    {
        //UserRepository usrrepository = new UserRepository(cstring.ConString);
        this.Insert(vehicleimage);
    }
    public void updateVehicleImage(VehicleImage vehicleimage)
    {
        this.Update(vehicleimage);
    }
    public int deleteVehicleImage(VehicleImage vehicleimage)
    {

        int id = this.Delete<VehicleImage>(vehicleimage);
        return id;
    }
    public List<VehicleImage> GetVehicleImages()
    {

        var vehicleImages = new List<VehicleImage>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            vehicleImages = connection.QueryAll<VehicleImage>().ToList();
            foreach (VehicleImage vehicleimage in vehicleImages)
            {
                vehicleimage.vehicle = connection.Query<Vehicle>(vehicleimage.vehicleId).FirstOrDefault();
                if (vehicleimage.vehicle != null)
                {
                    vehicleimage.vehicle.bid = connection.Query<Bid>(vehicleimage.vehicle.bidid).FirstOrDefault();
                }
            }
        }
        return vehicleImages;
    }
    public List<VehicleImage> GetImagesbyVehicleId(int vehicleId)
    {

        List<VehicleImage> vehicleImages = new List<VehicleImage>();
        using (var connection = new SqlConnection(sett.ConString))
        {

            vehicleImages = connection.QueryAll<VehicleImage>().Where(vi => vi.vehicleId == vehicleId).ToList();
            // Use the Query method to load the associated Vehicle objects for each VehicleImage.
            foreach (var vehicleImage in vehicleImages)
            {
                vehicleImage.vehicle = connection.Query<Vehicle>(vehicleImage.vehicleId).FirstOrDefault();

                if (vehicleImage.vehicle != null)
                {
                    vehicleImage.vehicle.bid = connection.Query<Bid>(vehicleImage.vehicle.bidid).FirstOrDefault();
                }
            }
        }
        return vehicleImages;
    }

    public List<VehicleImage> GetDefaultVehicleImages()
    {
        var vehicleImages = new List<VehicleImage>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            var sql = "SELECT * FROM [Auction].[dbo].[VehicleImage] WHERE defaultImage = 1";
            vehicleImages = connection.ExecuteQuery<VehicleImage>(sql).ToList();
            foreach (VehicleImage vehicleimage in vehicleImages)
            {
                  var sql2 = "SELECT * FROM [Auction].[dbo].[Vehicle] WHERE id = @Id";
            // Provide parameter value using an anonymous object or a Dictionary<string, object>
                var parameters = new { Id = vehicleimage.vehicleId };
                vehicleimage.vehicle = connection.ExecuteQuery<Vehicle>(sql2, parameters).FirstOrDefault();
                if (vehicleimage.vehicle != null)
                    {
                        var sql3 = "SELECT * FROM [Auction].[dbo].[Bid] WHERE id = @Id";
                        var parameters2 = new { Id = vehicleimage.vehicle.bidid };
                        vehicleimage.vehicle.bid = connection.ExecuteQuery<Bid>(sql3, parameters2).FirstOrDefault();
                    }
            }
            // Use RepoDb's Query method to filter by defaultImage = true
            return vehicleImages;
        }
    }



}