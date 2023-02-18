using EComm___Backend.Data;
using EComm___Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EComm___Backend.Controllers
{
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseContext dbContext;
        public DatabaseController(DatabaseContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        //Products Function
        [HttpGet]
        [Route("api/GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            
            return Ok(await dbContext.Products.ToListAsync());
        }

        [HttpGet]
        [Route("api/GetProduct/{productID:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid productID)
        {
            var product = await dbContext.Products.FindAsync(productID);
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost]
        [Route("api/AddProduct")]
        public async Task<IActionResult> AddProducts(AddUpdateProduct addProducts)
        {
            var product = new Product()
            {
                productID = new Guid(),
                productName = addProducts.productName,
                price = addProducts.price,
                stock = addProducts.stock,
                category = addProducts.category,
                shorDesc = addProducts.shorDesc,
                longDesc = addProducts.longDesc,
                imgURL = addProducts.imgURL,
                avgRating = addProducts.avgRating
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        [Route("api/UpdateProducts/{productID:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productID, AddUpdateProduct updateProducts)
        {
            var product = await dbContext.Products.FindAsync(productID);

            if (product != null )
            {
                product.productName = updateProducts.productName;
                product.price = updateProducts.price;
                product.stock = updateProducts.stock;
                product.category = updateProducts.category;
                product.shorDesc = updateProducts.shorDesc;
                product.longDesc = updateProducts.longDesc;
                product.imgURL = updateProducts.imgURL;


                await dbContext.SaveChangesAsync();

                return Ok(product);
                
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("api/DeleteProduct/{productID:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productID)
        {
            var product = await dbContext.Products.FindAsync(productID);
            if (product != null)
            {
                dbContext.Remove(product);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        //End Of Products Function


        //===============================================================================================


        //User Functionss

        [HttpGet]
        [Route("api/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {

            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("api/GetUser/{userID:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid userID)
        {
            var user = await dbContext.Users.FindAsync(userID);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> login(string email, string userPass)
        {
            var user = dbContext.Users.Where(u => u.email.Equals(email) && u.password.Equals(userPass));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        [Route("api/AddUsers")]
        public async Task<IActionResult> AddUser(AddUpdateUsers addUser)
        {
            var user = new User()
            {
                userID = new Guid(),
                firstName = addUser.firstName,
                lastName = addUser.lastName,
                userName = addUser.userName,
                email = addUser.email,
                password = addUser.password,
                userType = "Customer"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        [Route("api/AdminAddUsers")]
        public async Task<IActionResult> AdminAddUser(AdminAddUpdateUser addUser)
        {
            var user = new User()
            {
                userID = new Guid(),
                firstName = addUser.firstName,
                lastName = addUser.lastName,
                userName = addUser.userName,
                email = addUser.email,
                password = addUser.password,
                userType = addUser.userType
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        [Route("api/AdminUpdateUsers/{userID:guid}")]
        public async Task<IActionResult> AdminUpdateUser([FromRoute] Guid userID, AdminAddUpdateUser updateUser)
        {
            var user = await dbContext.Users.FindAsync(userID);
            if (user != null)
            {
                user.firstName = updateUser.firstName;
                user.lastName = updateUser.lastName;
                user.userName = updateUser.userName;
                user.email = updateUser.email;
                user.password = updateUser.password;
                user.userType = updateUser.userType;


                await dbContext.SaveChangesAsync();

                return Ok(user);

            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/UpdateUser/{userID:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userID, AddUpdateUsers updateUser)
        {
            var user = await dbContext.Users.FindAsync(userID);

            if (user != null)
            {
                user.firstName = updateUser.firstName;
                user.lastName = updateUser.lastName;
                user.userName = updateUser.userName;
                user.email = updateUser.email;
                user.password = updateUser.password;


                await dbContext.SaveChangesAsync();

                return Ok(user);

            }
            return NotFound();
        }


        [HttpDelete]
        [Route("api/DeleteUser/{userID:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userID)
        {
            var user = await dbContext.Users.FindAsync(userID);
            if (user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        //End of User Functions


        //===========================================================================================================

        /*
        //Cart Functions
        [HttpGet]
        [Route("api/GetCarts/cartID:guid")]
        public async Task<IActionResult> GetCart([FromRoute] Guid cartID)
        {   
                var cart = dbContext.Carts.FindAsync(cartID);
                if (cart != null)
                {
                    var cartItems = new CartItem();
                    foreach(var item in dbContext.CartItems)
                    {
                        if (item.cartID == cartID)
                        { 
                            
                        }
                    }
                }
                return Ok(await dbContext.CartItems.ToListAsync());
        }

            [HttpGet]
            [Route("api/GetUser/{userID:guid}")]
            public async Task<IActionResult> GetUser([FromRoute] Guid userID)
            {
                var user = await dbContext.Users.FindAsync(userID);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }


            [HttpPost]
            [Route("api/AddUsers")]
            public async Task<IActionResult> AddUser(AddUpdateUsers addUser)
            {
                var user = new User()
                {
                    userID = new Guid(),
                    firstName = addUser.firstName,
                    lastName = addUser.lastName,
                    userName = addUser.userName,
                    email = addUser.email,
                    password = addUser.password,
                    userType = "Users"
                };

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

                return Ok(user);
            }

            [HttpPut]
            [Route("api/UpdateUser/{userID:guid}")]
            public async Task<IActionResult> UpdateUser([FromRoute] Guid userID, AddUpdateUsers updateUser)
            {
                var user = await dbContext.Users.FindAsync(userID);

                if (user != null)
                {
                    user.firstName = updateUser.firstName;
                    user.lastName = updateUser.lastName;
                    user.userName = updateUser.userName;
                    user.email = updateUser.email;
                    user.password = updateUser.password;


                    await dbContext.SaveChangesAsync();

                    return Ok(user);

                }
                return NotFound();
            }


            [HttpDelete]
            [Route("api/DeleteUser/{userID:guid}")]
            public async Task<IActionResult> DeleteUser([FromRoute] Guid userID)
            {
                var user = await dbContext.Users.FindAsync(userID);
                if (user != null)
                {
                    dbContext.Remove(user);
                    await dbContext.SaveChangesAsync();
                    return Ok();
                }

                return NotFound();
           }*/
        
        //End of Cart Functions

        //===========================================================================================================

        //Order Functions

        /*
        [HttpGet]
        [Route("api/GetOrders")]
        public async Task<IActionResult> GetOrders()
        {

            return Ok(await dbContext.Orders.ToListAsync());
        }

        [HttpGet]
        [Route("api/GetOrder/{orderID:guid}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid orderID)
        {
            var order = await dbContext.Orders.FindAsync(orderID);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        
        [HttpPost]
        [Route("api/AddOrder/{userID:guid}")]
        public async Task<IActionResult> AddOrder([FromRoute] Guid userID, int )
        {
            var user = dbContext.Users.Find(userID);
            if (user != null) 
            {
                return NotFound();
            }
            else
            {
                
                var order = new Order()
                {
                    orderID = new Guid(),
                    User = user,
                    Created = DateTime.Now,
                };

                var orderItems = new OrderItem()
                {

                }

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

        }

        [HttpPut]
        [Route("api/UpdateUser/{userID:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userID, AddUpdateUsers updateUser)
        {
            var user = await dbContext.Users.FindAsync(userID);

            if (user != null)
            {
                user.firstName = updateUser.firstName;
                user.lastName = updateUser.lastName;
                user.userName = updateUser.userName;
                user.email = updateUser.email;
                user.password = updateUser.password;


                await dbContext.SaveChangesAsync();

                return Ok(user);

            }
            return NotFound();
        }


        [HttpDelete]
        [Route("api/DeleteUser/{userID:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userID)
        {
            var user = await dbContext.Users.FindAsync(userID);
            if (user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        */

        //End of Order Functions


        //============================================================================================================



        //Review Functions


        //End of Review Functions


    }
}
