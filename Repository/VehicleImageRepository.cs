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

public class VehicleImageRepository: BaseRepository<VehicleImage, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public VehicleImageRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertVehicleImage(VehicleImage vehicleimage)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(vehicleimage);
        }
        public  void updateVehicleImage(VehicleImage vehicleimage)
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
          
           var vehicleImages= new List<VehicleImage>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                vehicleImages = connection.QueryAll<VehicleImage>().ToList();
                foreach (VehicleImage vehicleimage in vehicleImages)
                {
                     vehicleimage.vehicle=connection.Query<Vehicle>(vehicleimage.vehicleId).FirstOrDefault();
                }
            } 
          return vehicleImages;
        }
        public VehicleImage GetImagesbyVehicleId(int id)
        {  
          
           var vehicleImages= new VehicleImage();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               vehicleImages = connection.Query<VehicleImage>(id).FirstOrDefault();
            }
          return vehicleImages;
        }
        
         
    }