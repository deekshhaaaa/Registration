using SQLite;

namespace Registration
{
    [Table("TbTransaction")]
    public class Transaction
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("TrnsID")]
        public int TrnsID { get; set; }

        [Column("MstrID")]
        public int MstrID { get; set; }

        [Column("Amt")]
        public decimal Amt { get; set; }

        [Column("DbtCdt")]
        public string DbtCdt { get; set; }

        [Column("TrnsDt")]
        public DateTime TrnsDt { get; set; }
    }
}