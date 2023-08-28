
    public class Setting
    {
        public string ConString { get; set; }

        public static void initializeRepoDb()
        {
           RepoDb.SqlServerBootstrap.Initialize();
        }
    }
