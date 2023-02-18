namespace EComm___Backend.Models
{
    public class AddUpdateProduct
    {
        public string productName { get; set; }

        public float price { get; set; }

        public int stock { get; set; }  

        public string category { get; set; }

        public string shorDesc { get; set; }

        public string longDesc { get; set; }

        public string imgURL { get; set; }

        public double avgRating { get; set; }
    }
}
