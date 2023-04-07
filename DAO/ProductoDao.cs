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
    public class ProductoDao
    {
        private readonly SqlConnection conexion;
        public ProductoDao()
        {
            conexion = Conexion.GetConnection();
        }
        public List<Producto> ListarProducto()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                string sql = "Select p.Id, p.Nombre, p.Precio, p.Fechacreacion, p.Foto, c.Nombre, p.Stock from Producto p INNER JOIN Categoria c on p.Categoriaid = c.Id";

                SqlCommand cmd = new SqlCommand(sql, conexion)
                {
                    CommandType = CommandType.Text,
                };
                conexion.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var producto = new Producto()
                    {
                        ProductoId = sdr.GetInt32(0),
                        Nombre = sdr.GetString(1),
                        Precio = sdr.GetDecimal(2),
                        FechaCreacion = sdr.GetDateTime(3),
                        Foto = sdr.IsDBNull(4) ? "" : sdr.GetString(4),
                        Categoria = sdr.GetString(5),
                        Stock = sdr.GetInt32(6)
                    };
                    lista.Add(producto);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

        public Producto ObtenerProducto(int productoId)
        {
            Producto p = null;
            try
            {
                string sql = $"Select p.Id, p.Nombre, p.Precio, p.Fechacreacion, p.Foto, c.Nombre, p.Stock, p.CategoriaId from Producto p INNER JOIN Categoria c on p.Categoriaid = c.Id Where p.Id={productoId}";

                SqlCommand cmd = new SqlCommand(sql, conexion)
                {
                    CommandType = CommandType.Text,
                };
                conexion.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    p = new Producto()
                    {
                        ProductoId = sdr.GetInt32(0),
                        Nombre = sdr.GetString(1),
                        Precio = sdr.GetDecimal(2),
                        FechaCreacion = sdr.GetDateTime(3),
                        Foto = sdr.IsDBNull(4) ? "" : sdr.GetString(4),
                        Categoria = sdr.GetString(5),
                        Stock = sdr.GetInt32(6),
                        CategoriaId = sdr.GetInt32(7)
                    };
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                conexion.Close();
            }
            return p;
        }
        public int RegistrarProducto(Producto p)
        {
            int res;
            try
            {
                string sql = $"Insert into Producto (Nombre, Precio, Fechacreacion, Foto, CategoriaId, Stock) values ('{p.Nombre}', {p.Precio}" +
                    $", '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{p.Foto}', {p.CategoriaId}, {p.Stock})";
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

        public int ActualizarProducto(Producto p)
        {
            int res;
            try
            {
                string sql = $"Update Producto set Nombre = '{p.Nombre}', Precio = {p.Precio}, Foto = '{p.Foto}', CategoriaId = {p.CategoriaId}, Stock = {p.Stock} WHERE Id = {p.ProductoId}";
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

        public int EliminarProducto(int pId)
        {
            int res;
            try
            {
                string sql = $"DELETE FROM Producto where Id = {pId}";
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
        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                string sql = "SELECT Id, Nombre FROM Categoria";
                SqlCommand cmd = new SqlCommand(sql, conexion)
                {
                    CommandType = CommandType.Text
                };
                conexion.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    categorias.Add(new Categoria
                    {
                        CategoriaId = sdr.GetInt32(0),
                        Nombre = sdr.GetString(1)
                    });
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                conexion.Close();
            }
            return categorias;
        }
    }
}
