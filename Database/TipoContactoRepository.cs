using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Modelos;

namespace Database
{
    public class TipoContactoRepository : RepositoryBase
    {

        public TipoContactoRepository(SqlConnection connection) : base(connection) { }

        public List<TipoContacto> GetList()
        {
            List<TipoContacto> list = new List<TipoContacto>();

            GetConnection().Open();

            SqlCommand command = new SqlCommand("Select Id,Name from TipoContacto",GetConnection());

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                list.Add(new TipoContacto
                {
                    Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1)
                });

            }

            reader.Close();
            reader.Dispose();

            GetConnection().Close();


            return list;
        }

    }
}
