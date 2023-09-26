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

public class VehicleRepository : BaseRepository<Vehicle, SqlConnection>
{

    //Setting cstring=new Setting();
    Setting sett = new Setting();
    public VehicleRepository(Setting sett) : base(sett.ConString)
    {
        this.sett = sett;
        DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
        DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()), true);

    }
    public void insertVehicle(Vehicle vehicle)
    {
        //UserRepository usrrepository = new UserRepository(cstring.ConString);
        this.Insert(vehicle);
    }
    public void updateVehicle(Vehicle vehicle)
    {

        this.Update(vehicle);
    }
    public int deleteVehicle(Vehicle vehicle)
    {

        int id = this.Delete<Vehicle>(vehicle);
        return id;
    }
    public List<Vehicle> GetVehicles()
    {

        var vehicless = new List<Vehicle>();
        using (var connection = new SqlConnection(sett.ConString))
        {
            vehicless = connection.QueryAll<Vehicle>().ToList();
            /* Do the stuffs for the people here */

            // Use the Query method to load the associated Vehicle objects for each VehicleImage.
            foreach (var vehicle in vehicless)
            {
                vehicle.bid = connection.Query<Bid>(vehicle.bidid).FirstOrDefault();
            }
        }
        return vehicless;
    }
    public Vehicle GetBidDetails(int id)
    {

        var vehicless = new Vehicle();
        using (var connection = new SqlConnection(sett.ConString))
        {

            vehicless = connection.Query<Vehicle>(id).FirstOrDefault();
        }
        return vehicless;
    }


}