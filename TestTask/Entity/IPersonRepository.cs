using System.Collections.Generic;
using TestTask.Models;

namespace TestTask.Entity
{
	public interface IPersonRepository
	{
		List<Person> GetAllPersons();
		List<Person> GetStructedManagers(int id);
		Person GetPerson(int id);
		void Delete(int id);
		void Add(string name, string position, int tableNumber, string order, int managerTableNumber);
		void Update(int id, string name, string position, int tableNumber, string order, int managerTableNumber);
	}
}
