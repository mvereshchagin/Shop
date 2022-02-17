using Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using WritableJsonConfiguration;

namespace Shop
{
    internal class Project
    {
        protected const string APP_SETTINGS_USERNAME = "UserName";

        #region messages
        protected const string MSG_INVALID_ACTION = "Invalid input. Please, try one more time";
        protected const string MSG_CHOOSE_ACTION = "Choose action:";
        protected const string MSG_WELCOME = "Welcome, {0}";
        protected const string MSG_DELIMITER = "-------------------------------------------";
        #endregion

        protected string connectionString = null!;
        protected IConfiguration appConfig = null!;
        protected User? User = null;
        protected DbUtils dbUtils = null!;

        public bool CheckConnection()
        {
            using (ShopContext db = new ShopContext(this.connectionString))
            {
                try
                {
                    if (!db.Database.CanConnect())
                    {
                        Console.WriteLine("Oops! Can not connect to db");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Everything is OK");
                        return false;
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Oops! Can not connect to db");
                    return false;
                }
            }
        }

        /// <summary>
        /// This method initialize the project
        /// </summary>
        public void Initialize()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            this.appConfig = configurationBuilder.Add<WritableJsonConfigurationSource>(
                (Action<WritableJsonConfigurationSource>)(s =>
                {
                    s.FileProvider = null;
                    s.Path = "appsettings.json";
                    s.Optional = false;
                    s.ReloadOnChange = true;
                    s.ResolveFileProvider();
                })).Build();

            this.connectionString = appConfig.GetConnectionString("Shop");
            this.dbUtils = new DbUtils(this.connectionString);
        }

        public void Start()
        {
            Console.WriteLine(String.Format(MSG_WELCOME, this.User.Name));
            Console.WriteLine(MSG_DELIMITER);
            Console.WriteLine(MSG_CHOOSE_ACTION);
            Console.WriteLine(ActionUtils.PrintMessage());
            Action action = ActionUtils.ReadAction(MSG_INVALID_ACTION);
            switch (action)
            {
                case Action.ListProducts:
                    ListProducts();
                    break;
                case Action.AddReview:
                    AddReview();
                    break;
                case Action.AddProduct:
                    AddProduct();
                    break;
                default:
                    break;
            }
        }  
        

        //public void PrintProducts()
        //{

        //}

        //public void PrintReviews(User? user, Product? product)
        //{

        //}

        public void AuthorizeOrRegister()
        {
            string? login = null;
            try
            {
                login = appConfig[APP_SETTINGS_USERNAME];
            }
            catch { }

            if (!String.IsNullOrEmpty(login))
                this.User = dbUtils.FindUser(login);

            if (this.User is null)
            {
                login = ConsoleUtils.ReadString(minLength: 3, maxLength: 50, fieldName: "login");
                var password = ConsoleUtils.ReadString(minLength: 3, maxLength: 50, fieldName: "password");

                var user = dbUtils.AddUser(login: login, password: password, name: login);

                appConfig[APP_SETTINGS_USERNAME] = login;
                this.User = user;
            }
        }

        public void ListProducts()
        {
            Console.WriteLine("ListProducts");
        }

        public void AddReview()
        {
            Console.WriteLine("AddReview");
        }

        public void AddProduct()
        {
            var name = ConsoleUtils.ReadString(maxLength: 128, fieldName: "Name");
            var desc = ConsoleUtils.ReadString(maxLength: 512, fieldName: "Description");
            var price = ConsoleUtils.ReadInt(minValue: 0, maxValue: 100000, fieldName: "Price");

            this.dbUtils.AddProduct(name: name, description: desc, price: price);
        }
    }
}
