using Proyecto_DSWI.Database;
using Proyecto_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DSWI.DAO
{
    public class VentaDao
    {
        private readonly SqlConnection conexion;
        public VentaDao()
        {
            conexion = Conexion.GetConnection();
        }
        public int RegistrarVenta(Venta v,int stock, int idProducto)
        {
            int res;
            try
            {
                string sql = $"Insert into Venta (DiaVenta, Subtotal, Iva, Total) " +
                    $"values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', {v.Subtotal}, {v.Iva}, {v.Total});" +
                    $"Update Producto set Stock = {stock} WHERE Id = {idProducto}";
                
                SqlCommand cmd = new SqlCommand(sql, conexion)
                {
                    CommandType = CommandType.Text,
                };
                conexion.Open();
                res = cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                res = 0;
            }
            finally
            {
                conexion.Close();
            }
            return res;
        }
    }
}
