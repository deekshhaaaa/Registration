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
            strName = strName.ToUpper();
            return await con.Table<Customer>().Where(x => x.Name.ToUpper().StartsWith(strName)).ToListAsync();
        }

        public async Task<Customer> getCustomerbyId(int _id)
        {
            return await con.Table<Customer>().Where(x => x.Id == _id).FirstOrDefaultAsync();
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
        #endregion
    }
}