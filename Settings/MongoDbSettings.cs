namespace checkoutapi.Settings
{
    public class MongoDbSettings
    {
        public string Host {get;set;}
        public int Port {get;set;}
        public string User {get;set;}
        public string Password {get;set;}
        public string ConnectionString {get
           {

            return $"mongodb://{User}:Pass#word1@{Host}:{Port}";
            } 
        }

    }
}
