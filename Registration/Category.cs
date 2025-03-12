using SQLite;
namespace Registration
{
    [Table ("tbCategory")]
   public class Category
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("CatID")]
        public int CatID { get; set; }
        [Column("CatName")]
        public string CatName { get; set; }
    }
}
