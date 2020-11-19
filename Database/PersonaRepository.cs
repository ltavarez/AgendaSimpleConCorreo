using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Modelos;

namespace Database
{
    public class PersonaRepository : RepositoryBase , IRepository<Persona>
    {

        public PersonaRepository(SqlConnection connection) : base(connection) { }


        public bool Add(Persona item)
        {
            SqlCommand command = new SqlCommand("insert into Personas(Nombre,Apellido,Telefono,IdTipoContacto)" +
                                                "values (@name,@lastname,@phone,@tipocontacto)", GetConnection());

            command.Parameters.AddWithValue("@name", item.Nombre);
            command.Parameters.AddWithValue("@lastname", item.Apellido);
            command.Parameters.AddWithValue("@phone", item.Telefono);
            command.Parameters.AddWithValue("@tipocontacto", item.IdTipoContacto);


            return ExecuteDml(command);

        }

        public bool Edit(Persona item)
        {
            SqlCommand command = new SqlCommand("update Personas set Nombre=@name,Apellido=@lastname,Telefono=@phone,IdTipoContacto=@tipocontacto where Id = @id", GetConnection());

            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@name", item.Nombre);
            command.Parameters.AddWithValue("@lastname", item.Apellido);
            command.Parameters.AddWithValue("@phone", item.Telefono);
            command.Parameters.AddWithValue("@tipocontacto", item.IdTipoContacto);

            return ExecuteDml(command);


        }

        public bool SavePhoto(int id, string destination)
        {

            SqlCommand command = new SqlCommand("update Personas set FotoPerfil=@foto where Id = @id", GetConnection());

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@foto", destination);

            return ExecuteDml(command);
        }

        public int GetLastId()
        {
            int lastId = 0;

            GetConnection().Open();

            SqlCommand command = new SqlCommand("select max(Id) as Id from Personas",GetConnection());

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                lastId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            }

            reader.Close();
            reader.Dispose();


            GetConnection().Close();

            return lastId;

        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("delete Personas where Id = @id",GetConnection());


            command.Parameters.AddWithValue("@id", id);

            return ExecuteDml(command);
        }

        public DataTable List()
        {
            SqlDataAdapter query = new SqlDataAdapter("Select p.Id as 'Codigo',p.Nombre as 'Nombre',p.Apellido as 'Apellido',p.Telefono as 'Telefono', tc.Name as 'Tipo de contacto' from Personas p inner join TipoContacto tc on p.IdTipoContacto = tc.Id ",GetConnection());
            return LoadData(query);
        }

        public Persona GetById(int id)
        {
            GetConnection().Open();

            Persona persona = new Persona();

            SqlCommand command = new SqlCommand(" select p.Id, p.Nombre, p.Apellido, p.Telefono,tc.Name as 'TipoContacto',p.FotoPerfil as 'Foto de perfil' from Personas p inner join TipoContacto tc on p.IdTipoContacto = tc.Id where p.Id=@id", GetConnection());
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                persona.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                persona.Nombre = reader.IsDBNull(1) ? "" : reader.GetString(1);
                persona.Apellido = reader.IsDBNull(2) ? "" : reader.GetString(2);
                persona.Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3);
                persona.TipoContacto = reader.IsDBNull(4) ? "" : reader.GetString(4);
                persona.FotoPerfil = reader.IsDBNull(5) ? "" : reader.GetString(5);


            }

            reader.Close();
            reader.Dispose();
            GetConnection().Close();

            return persona;
        }
    }
}
