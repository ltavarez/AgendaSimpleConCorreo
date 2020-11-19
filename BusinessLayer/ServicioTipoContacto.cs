using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Modelos;

namespace BusinessLayer
{
    public class ServicioTipoContacto
    {

        private readonly TipoContactoRepository _repository;
        public ServicioTipoContacto(SqlConnection connection)
        {
            _repository = new TipoContactoRepository(connection);

        }

        public List<TipoContacto> GetList()
        {
            return _repository.GetList();
        }

    }
}
