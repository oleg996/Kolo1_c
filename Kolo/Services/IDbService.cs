using Kolo.Model;
namespace Kolo.Services;

public interface IDbService
{
    Task<Boolean> does_del_exists(int id);
    Task<DateTime> getDeliverytime(int id);

    Task<Customer> get_customer_data(int id);

    Task<Driver> get_driver_data(int id);

    Task<List<Product>> get_products(int id);

    Task<Boolean> does_cus_exists(int id);

    Task<Boolean> does_driv_exists(string lic);

    Task<Boolean> does_item_exists(string name);

    Task addItem(Delivery_add delivery_Add);
}