namespace EComm___Backend.Models
{
    public class User
    {
        public Guid userID { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string userName { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        public string userType { get; set; }

    }

    public class Product
    {
        public Guid productID { get; set; }

        public string productName { get; set; }

        public float price { get; set; }

        public int stock { get; set; }

        public string category { get; set; }

        public string shorDesc { get; set; }

        public string longDesc { get; set; }

        public string imgURL { get; set; }

        public double avgRating { get; set; }

    }

    public class Review
    {
        public Guid reviewID { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        public int rating { get; set; }

        public string reviewMsg { get; set; }
    }

    public class Order
    {
        public Guid orderID { get; set; }

        public User User { get; set; }

        public float payment { get; set; }
        public float totalPrice { get; set; }
        public DateTime? Created { get; set; }

        public ICollection<OrderItem> orderItems { get; set; }
    }

    public class OrderItem
    {
        public Guid orderItemId { get; set; }
        public int quantity { get; set; }
        public Product Product { get; set; }
    }

    public class Cart
    {
        public Guid cartID { get; set; }

        public User User { get; set; }

        public float subTotal { get; set; }

        public ICollection<CartItem> cartItems { get; set; }
    }

    public class CartItem
    {
        public Guid cartItemID { get; set; }

        public Cart cart { get; set; }

        public int quantity { get; set; }

        public Product Product { get; set; }

    }
}
