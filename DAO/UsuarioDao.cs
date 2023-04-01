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
    public class UsuarioDao
    {
        private readonly SqlConnection conexion;
        public UsuarioDao()
        {
            conexion = Conexion.GetConnection();
        }
        public Usuario Login(Usuario u)
        {
            Usuario user = null;
            try
            {
                string sql = $"Select * from Usuario where NombreUsuario = '{u.NombreUsuario}' and Password = '{u.Password}'";
                SqlCommand cmd = new SqlCommand(sql, conexion)
                {
                    CommandType = CommandType.Text,
                };
                conexion.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    user = new Usuario()
                    {
                        UsuarioId = sdr.GetInt32(0),
                        NombreUsuario = sdr.GetString(1),
                        NombreCompleto = sdr.GetString(2),
                        Password = sdr.GetString(3),
                        TipoUsuarioId = sdr.GetInt32(4)
                    };
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
            return user;
        }
    }
}
