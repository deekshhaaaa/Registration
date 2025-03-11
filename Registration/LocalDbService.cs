using SQLite;

namespace Registration
{
    public class LocalDbService
    {
        private const string DbName = "MainDb.db3";
        private readonly SQLiteAsyncConnection con;

        public LocalDbService()
        {
            con = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            con.CreateTableAsync<Customer>();
            con.CreateTableAsync<Transaction>();
        }
        #region Customer
        public async Task Create(Customer objCustomer)
        {
            await con.InsertAsync(objCustomer);
        }

        public async Task Update(Customer objCustomer)
        {
            await con.UpdateAsync(objCustomer);
        }

        public async Task Delete(Customer objCustomer)
        {
            await con.DeleteAsync(objCustomer);
        }
        public async Task<List<Customer>> GetCustomers()
        {
            return await con.Table<Customer>().ToListAsync();
        }

        public async Task<List<Customer>> GetCustomersByName(string strName)
        {
            if (string.IsNullOrEmpty(strName))
            {
                return new List<Customer>();
            }

            strName = strName.ToUpper();
            return await con.Table<Customer>().Where(x => x.Name.ToUpper().StartsWith(strName)).ToListAsync();
        }
        public async Task<Customer> getCustomerbyNameAndNumber(string _name, string _mobile)
        {
            return await con.Table<Customer>().Where(x => x.Name == _name && x.Mobile == _mobile).FirstOrDefaultAsync();
        }

        public async Task<Customer> getCustomerbyId(int _id)
        {
            return await con.Table<Customer>().Where(x => x.Id == _id).FirstOrDefaultAsync();
        }
        public async Task DeleteAllCustomers()
        {
            await con.ExecuteAsync("delete from TbCustomer");
        }
        #endregion

        #region Transaction
        public async Task Create(Transaction objTrns)
        {
            try
            {
                await con.InsertAsync(objTrns);
            }
            catch (Exception ex)
            {
                con.CreateTableAsync<Transaction>();
                await con.InsertAsync(objTrns);
            }
        }
        public async Task<List<Transaction>> GetTransactions()
        {
            return await con.Table<Transaction>().ToListAsync();
        }
        public async Task<List<Transaction>> getTransactionByMstrID(int M_ID)
        {
            return await con.Table<Transaction>().Where(x => x.MstrID == M_ID).ToListAsync();
        }
        public async Task<Transaction> getTransactionbyID(int T_id)
        {
            return await con.Table<Transaction>().Where(x => x.TrnsID == T_id).FirstOrDefaultAsync();
        }
        public async Task<decimal> getBalanceAmtbyID(int MstrID)
        {
            return await con.ExecuteScalarAsync<decimal>("select sum(Amt) from TbTransaction where MstrID='" + MstrID + "'");
        }
        public async Task<decimal> getBalanceAmt()
        {
            return await con.ExecuteScalarAsync<decimal>("select sum(Amt) from TbTransaction");
        }
        public async Task DeleteTransaction(int TrnsID)
        {
            await con.ExecuteAsync("delete from TbTransaction where TrnsID='" + TrnsID + "'");
        }
        public async Task DeleteAllTrns()
        {
            await con.ExecuteAsync("delete from TbTransaction");
        }
        #endregion
    }
}