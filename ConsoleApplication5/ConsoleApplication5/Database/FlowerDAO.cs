using FlowersGirl.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersGirl.Database
{
    public class FlowerDAO
    {
        private const string FLOWER_ID_FIELD = "id";

        private const string FLOWER_NAME_FIELD = "name";

        private const string FLOWER_PRICE_FIELD = "price";

        private const string FLOWER_CURRENCY_FIELD = "currency";

        private const string FLOWER_COLOUR_FIELD = "colour";

        private const string FLOWER_BUCKET_PRICE_FIELD = "price";

        private const string FLOWER_BUCKET_CURRENCY_FIELD = "currency";

        private const string FLOWER_BUCKET_FLOWER_ID_FIELD = "flower_id";

        private const string FLOWER_BUCKET_BUCKET_ID_FIELD = "bucket_id";

        private const string TOP5_EXPENSIVE_FLOWERS_PROCEDURE = "get_top5_expensive_flowers";

        private DbConnection DbConnection = new DbConnection();

        public void InitOrReInit()
        {
            string initQuery = Properties.Resources.Init;
            string createProcedureQuery = Properties.Resources.CreateProcedure;
            DbConnection.ExecuteNonResultOperation(initQuery, new SqlParameter[0]);
            DbConnection.ExecuteNonResultOperation(createProcedureQuery, new SqlParameter[0]);
        }

        public int InsertFlower(Flower src)
        {
            string query = string.Format("INSERT INTO flower (name, price, currency, colour) output INSERTED.ID " +
                "VALUES (@{0},@{1},@{2},@{3})", FLOWER_NAME_FIELD, FLOWER_PRICE_FIELD, FLOWER_CURRENCY_FIELD, FLOWER_COLOUR_FIELD);

            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter(FLOWER_NAME_FIELD, SqlDbType.VarChar);
            parameters[0].Value = src.Name;
            parameters[1] = new SqlParameter(FLOWER_PRICE_FIELD, SqlDbType.Int);
            parameters[1].Value = src.Price;
            parameters[2] = new SqlParameter(FLOWER_CURRENCY_FIELD, SqlDbType.VarChar);
            parameters[2].Value = src.Currency;
            parameters[3] = new SqlParameter(FLOWER_COLOUR_FIELD, SqlDbType.VarChar);
            parameters[3].Value = src.Colour;

            return DbConnection.ExecuteResultIdOperation(query, parameters);
        }

        public void DeleteFlower(int id)
        {
            string query = string.Format("DELETE FROM flower WHERE id = @{0}", FLOWER_ID_FIELD);

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter(FLOWER_ID_FIELD, SqlDbType.Int);
            parameters[0].Value = id;

            DbConnection.ExecuteNonResultOperation(query, parameters);
        }

        public void UpdateFlowerPrice(int price, Currency currency, int flowerId)
        {
            string query = string.Format("UPDATE flower SET price = @{0}, currency = @{1} WHERE id = @{2}", 
                FLOWER_PRICE_FIELD, FLOWER_CURRENCY_FIELD, FLOWER_ID_FIELD);

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter(FLOWER_PRICE_FIELD, SqlDbType.Int);
            parameters[0].Value = price;
            parameters[1] = new SqlParameter(FLOWER_CURRENCY_FIELD, SqlDbType.VarChar);
            parameters[1].Value = currency;
            parameters[2] = new SqlParameter(FLOWER_ID_FIELD, SqlDbType.Int);
            parameters[2].Value = flowerId;

            DbConnection.ExecuteNonResultOperation(query, parameters);
        }

        public List<Flower> FindFlowerByName(string name)
        {
            List<Flower> result = new List<Flower>();
            string query = string.Format("SELECT FROM flower WHERE name = @{0}", FLOWER_NAME_FIELD);

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter(FLOWER_NAME_FIELD, SqlDbType.VarChar);
            parameters[0].Value = name;

            DataTable dataTable = DbConnection.ExecuteResultOperation(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(CreateFlower(row));
            }

            return result;
        }

        public List<Flower> FindAllFlowers()
        {
            List<Flower> result = new List<Flower>();
            string query = "SELECT * FROM flower";

            SqlParameter[] parameters = new SqlParameter[0];

            DataTable dataTable = DbConnection.ExecuteResultOperation(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(CreateFlower(row));
            }

            return result;
        }

        private Flower CreateFlower(DataRow src)
        {
            Flower dest = Flower.CreateFlower(src[FLOWER_NAME_FIELD].ToString());
            dest.Price = int.Parse(src[FLOWER_PRICE_FIELD].ToString());
            dest.Currency = (Currency)Enum.Parse(typeof(Currency), src[FLOWER_CURRENCY_FIELD].ToString());
            dest.Colour = (Colour)Enum.Parse(typeof(Colour), src[FLOWER_COLOUR_FIELD].ToString());
            return dest;
        }

        public List<Flower> FindTop5ExpensiveFlowersByName(string name)
        {
            List<Flower> result = new List<Flower>();

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@" + FLOWER_NAME_FIELD, SqlDbType.VarChar);
            parameters[0].Value = name;

            DataTable dataTable = DbConnection.ExecuteProcedure(TOP5_EXPENSIVE_FLOWERS_PROCEDURE, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(CreateFlower(row));
            }

            return result;
        }

        public void InsertFlowerBucket(FlowersBucket bucket)
        {
            List<int> flowerIds = new List<int>();
            foreach (var flower in bucket.Flowers)
            {
                flowerIds.Add(InsertFlower(flower));
            }

            string query = string.Format("INSERT INTO flower_bucket (price, currency) output INSERTED.ID " +
                "VALUES (@{0},@{1})", FLOWER_BUCKET_PRICE_FIELD, FLOWER_BUCKET_CURRENCY_FIELD);

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter(FLOWER_PRICE_FIELD, SqlDbType.Int);
            parameters[0].Value = bucket.Price;
            parameters[1] = new SqlParameter(FLOWER_CURRENCY_FIELD, SqlDbType.VarChar);
            parameters[1].Value = bucket.Currency;

            int bucketId = DbConnection.ExecuteResultIdOperation(query, parameters);

            string flowerBucketQuery = string.Format("INSERT INTO flower_bucket_flower (flower_id, bucket_id) VALUES (@{0},@{1})", 
                FLOWER_BUCKET_FLOWER_ID_FIELD, FLOWER_BUCKET_BUCKET_ID_FIELD);

            foreach (var flowerId in flowerIds)
            {
                SqlParameter[] parametersFBQuery = new SqlParameter[2];
                parametersFBQuery[0] = new SqlParameter(FLOWER_BUCKET_FLOWER_ID_FIELD, SqlDbType.Int);
                parametersFBQuery[0].Value = flowerId;
                parametersFBQuery[1] = new SqlParameter(FLOWER_BUCKET_BUCKET_ID_FIELD, SqlDbType.Int);
                parametersFBQuery[1].Value = bucketId;

                DbConnection.ExecuteNonResultOperation(flowerBucketQuery, parametersFBQuery);
            }
        }
    }
}
