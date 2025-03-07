using SQLite;

namespace Registration
{
    [Table("TbCustomer")]
    public class Customer
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Mobile")]
        public string Mobile { get; set; }
        
        [Column ("Address")]
        public string Address { get; set; }
    }
}