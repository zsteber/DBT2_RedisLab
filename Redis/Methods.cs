using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Literals.Enums;
using System.Runtime.Intrinsics.X86;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Redis
{

    internal class Methods
    {

        public Methods()
        {

        }

        public string CreateEmployee()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:12000");

            var db = redis.GetDatabase();
            var ft = db.FT();
            var json = db.JSON();

            var user1 = new
            {
                name = "Paul Jones",
                HireDate = "2000",
                ID = 1
            };

            var user2 = new
            {
                name = "Jimothy Parker",
                HireDate = "2019",
                ID = 2
            };

            var user3 = new
            {
                name = "Peter Griffin",
                HireDate = "1997",
                ID = 3
            };

            var schema = new Schema()
            .AddTextField(new FieldName("$.name", "name"))
            .AddTagField(new FieldName("$.HireDate", "Hire Date"))
            .AddNumericField(new FieldName("$.ID", "ID"));

            ft.Create(
            "idx:users",
            new FTCreateParams().On(IndexDataType.JSON).Prefix("user:"),
            schema);

            json.Set("user:1", "$", user1);
            json.Set("user:2", "$", user2);
            json.Set("user:3", "$", user3);

            return "New Employee Added";
        }

        public string ReadEmployee()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:12000");
            var dbRead = redis.GetDatabase();
            var ftRead = dbRead.FT();
            var jsonRead = dbRead.JSON();

            var res = ftRead.Search("idx:users", new Query("Paul @ID:[1 3]")).Documents.Select(x => x["json"]);
            Console.WriteLine(string.Join("\n", res));
            return "";
        }

        public string UpdateEmployee()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:12000");
            var dbUpdate = redis.GetDatabase();
            var ftUpdate = dbUpdate.FT();
            var jsonUpdate = dbUpdate.JSON();

            var user1 = new
            {
                name = "Paul Jones",
                HireDate = "2000",
                ID = 1
            };

            var schema = new Schema()
            .AddTextField(new FieldName("$.name", "name"))
            .AddTagField(new FieldName("$.HireDate", "Hire Date"))
            .AddNumericField(new FieldName("$.ID", "ID"));

            ftUpdate.Create(
            "idx:users",
            new FTCreateParams().On(IndexDataType.JSON).Prefix("user:"),
            schema);

            jsonUpdate.Set("user:1", "$", user1);

            return "User Edited";
        }

        public string DeleteEmployee()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:12000");
            IDatabase db = redis.GetDatabase();
            db.KeyDelete("Paul Jones");
            return "";
        }

    }
}