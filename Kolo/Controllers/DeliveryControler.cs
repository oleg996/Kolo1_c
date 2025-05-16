using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Kolo.Model;
using Kolo.Services;


namespace Kolo.Controllers
{
    [Route("api/deliveries")]
    [ApiController]
    public class DeliveryControler : ControllerBase
    {
        private readonly IConfiguration _confyguration;

        private readonly IDbService _dbservice;

        public DeliveryControler(IConfiguration configuration, IDbService Dbservice)
        {
            _confyguration = configuration;
            _dbservice = Dbservice;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> getdata(int id)
        {
            if (!await _dbservice.does_del_exists(id))
                return NotFound("no such delivery");



            DateTime dateTime = await _dbservice.getDeliverytime(id);


            Customer c = await _dbservice.get_customer_data(id);


            Driver d = await _dbservice.get_driver_data(id);


            List<Product> p = await _dbservice.get_products(id);

            return Ok(new Delivery() { date = dateTime, customer = c, driver = d, products = p });
        }

        [HttpPost]
        public async Task<IActionResult> addItem(Delivery_add delivery_Add)
        {
            if (await _dbservice.does_del_exists(delivery_Add.deliveryId))
                return BadRequest("such delivery exists");

             if (!await _dbservice.does_cus_exists(delivery_Add.customerId))
                return NotFound("customer not exists");


            if (!await _dbservice.does_driv_exists(delivery_Add.licenceNumber))
                return NotFound("driver not exists");


            foreach (Product p in delivery_Add.products) {
                if (!await _dbservice.does_item_exists(p.name))
                {
                    return NotFound("item "+p.name+" not exists");
                }


            }

            await _dbservice.addItem(delivery_Add);


            return Created();
        }


    }
}