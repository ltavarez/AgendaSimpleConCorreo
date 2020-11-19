using Database;
using Database.Modelos;
using EmailHandler;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class ServicioPersona : IServicios<Persona>
    {

        private readonly PersonaRepository _repository;
        private readonly EmailSender _emailSender;

        public ServicioPersona(SqlConnection connection)
        {
            _repository = new PersonaRepository(connection);
            _emailSender = new EmailSender();

        }

        public bool Add(Persona item)
        {
            bool response = _repository.Add(item);

            if (response)
            {
                _emailSender.SendEmail("phpitladiplomado@gmail.com", "Nuevo contacto", "Se ha agregado este neuvo contacto: " + item.Nombre + " " + item.Apellido);
            }

            return response;
        }

        public bool Edit(Persona item)
        {
            return _repository.Edit(item);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public Persona GetById(int id)
        {
            return _repository.GetById(id);
        }

        public DataTable GetAll()
        {
            return _repository.List();
        }

        public bool SavePhoto(int id, string destination)
        {
            return _repository.SavePhoto(id, destination);
        }

        public int GetLastId()
        {
            return _repository.GetLastId();
        }
    }
}
