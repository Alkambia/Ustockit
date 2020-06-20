using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Repositories
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly IDbConnection _db;
        public RepositoryBase(IDbConnection db)
        {
            _db = db;
        }
        public async Task InsertProduct(ProductModel model)
        {
            await Task.Run(() => {
                
                using (var c = new MySqlConnection(_db.ConnectionString))
                {
                    var sql = @"INSERT INTO product (`ProductNo`,`Description`,`Price`) values(@ProductNo,@Description,@Price)";
                    c.Execute(sql, model, commandTimeout: 30);
                }
            });
            
        }
        public void Dispose()
        {
            _db.Close();
        }
    }
}
