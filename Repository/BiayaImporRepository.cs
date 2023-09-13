using Dapper;
using System;
using ILCSTest.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ILCSTest.Repository
{
    public class BiayaImporRepository
    {
       public string Insert(BiayaImpor bi)
        {
            string connString = "Server=localhost;Database=ILCSTest;Trusted_Connection=True;;MultipleActiveResultSets=true";
            string sql = "insert into biaya_impor(id_simulasi, kode_barang, Uraian_barang, bm, Nilai_komoditas, Nilai_bm, Waktu_Insert) " +
                "values(@id_simulasi, @kode_barang, @Uraian_barang, @Bm, @Nilai_komoditas, @Nilai_bm, @Waktu_Insert) ";

            try
            {

                using (IDbConnection db = new SqlConnection(connString))
                {
                    string insertQuery = sql;

                    var result = db.Execute(insertQuery, new
                    {
                        bi.id_simulasi,
                        bi.kode_barang,
                        bi.Uraian_barang,
                        bi.Bm,
                        bi.Nilai_komoditas,
                        bi.Nilai_bm,
                        bi.Waktu_Insert
                    });
                }
                return "Success Add Data";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
