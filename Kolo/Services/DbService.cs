using System.Data;
using System.Data.Common;
using Kolo.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Kolo.Services;

public class DbService : IDbService
{
    private readonly IConfiguration _configuration;
    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<DateTime> getDeliverytime(int id)
    {

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select date from Delivery where delivery_id  = @id", connection);


        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                return reader.GetDateTime(0);



            }
        }



        return new DateTime();
    }
    public async Task<Boolean> does_del_exists(int id)
    {

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select count(*) from Delivery where delivery_id  = @id", connection);


        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                if (reader.GetInt32(0) == 1)
                    return true;


            }
        }



        return false;

    }


    public async Task<Customer> get_customer_data(int id)
    {


        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select first_name ,last_name ,date_of_birth  from Customer c  where c.customer_id = (select customer_id  from Delivery where delivery_id  = @id)", connection);


        
        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {

                Customer c = new Customer()
                {
                    FirstName = reader.GetString(0),
                    LastName = reader.GetString(1),

                    dateOfBirth = reader.GetDateTime(2)

                };



                

                return c;
            }
        }



        return null;


    }

    public async Task<Driver> get_driver_data(int id)
    {


        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select first_name ,last_name ,licence_number   from Driver d   where d.driver_id = (select driver_id  from Delivery where delivery_id  = @id)", connection);


        Driver d = new Driver();

        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                d.FirstName = reader.GetString(0);

                d.LastName = reader.GetString(1);

                d.LicenseNumber = reader.GetString(2);

                return d;
            }
        }



        return d;


    }

    public async Task<List<Product>> get_products(int id)
    {
        List<Product> products = new List<Product>();

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select name,price ,amount  from Product p , Product_Delivery pd  where p.product_id  = pd.product_id and pd.delivery_id  = @id", connection);




        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Product p = new Product();
                p.name = reader.GetString(0);

                p.Price = (double)reader.GetDecimal(1);
                p.amount = reader.GetInt32(2);

                products.Add(p);

            }
        }



        return products;


    }


    public async Task<Boolean> does_cus_exists(int id)
    {

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select count(*) from Customer where customer_id  = @id", connection);


        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                if (reader.GetInt32(0) == 1)
                    return true;


            }
        }



        return false;

    }

    public async Task<Boolean> does_driv_exists(string lic)
    {

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select count(*) from Driver where licence_number   = @id", connection);


        command.Parameters.AddWithValue("@id", lic);

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                if (reader.GetInt32(0) == 1)
                    return true;


            }
        }



        return false;

    }

    public async Task<Boolean> does_item_exists(string name)
    {

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("select count(*) from Product where name = @id", connection);


        command.Parameters.AddWithValue("@id", name);

        await connection.OpenAsync();

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                if (reader.GetInt32(0) == 1)
                    return true;


            }
        }



        return false;

    }

    public async Task addItem(Delivery_add delivery_Add)
    {

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand("INSERT INTO  Delivery Values(@did,@cid,(select driver_id from Driver where licence_number = @dli),@dt) ", connection);


        command.Parameters.AddWithValue("@did", delivery_Add.deliveryId);

        command.Parameters.AddWithValue("@cid", delivery_Add.customerId);

        command.Parameters.AddWithValue("@dli", delivery_Add.licenceNumber);

        command.Parameters.AddWithValue("@dt", DateTime.Now);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();



        



        command.CommandText = "INSERT INTO  Product_Delivery Values((select p.product_id from Product p where p.name  = @pname),@did,@am)";


        foreach (Product p in delivery_Add.products)
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@pname", p.name);
            command.Parameters.AddWithValue("@did", delivery_Add.deliveryId);
            command.Parameters.AddWithValue("@am", p.amount);

            await command.ExecuteNonQueryAsync();


        }


    }
}