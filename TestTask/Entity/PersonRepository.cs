using System.Collections.Generic;
using System.Linq;
using TestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Entity
{
	public class PersonRepository : IPersonRepository
	{
		private readonly PersonContext Context;

		public PersonRepository (PersonContext context)
		{
			this.Context = context;
		}

		public void Delete(int id)
		{
			var person = Context.Persons.Single(p => p.Id == id);
			person.IsRemoved = true;
			Context.SaveChanges();
		}

		public List<Person> GetAllPersons()
		{
			var persons = Context.Persons.Where(p => !p.IsRemoved).ToList();
			return persons;
		}

		public List<Person> GetStructedManagers(int id)
		{
			var managers = new List<Person>();
			var person = Context.Persons.Include(c=>c.Manager).Single(p => p.Id == id);
			managers.Add(person);
			while ((person.Manager != null))
			{
				if(person.Manager.IsRemoved)
				{
					break;
				}
				managers.Add(person.Manager);
				person = Context.Persons.Include(c => c.Manager).Single(p => p.Id == person.Manager.Id);
			}
			managers.Reverse();
			return managers;
		}

		public Person GetPerson(int id)
		{
			var person = Context.Persons.Single(p => p.Id == id);
			return person;
		}

		public void Add(string name, string position, int tableNumber, string order, int managerTableNumber)
		{
			var manager = new Person();
			if (managerTableNumber != 0)
			{
				manager = Context.Persons.Single(p => p.TableNumber == managerTableNumber);
			}
			else
			{
				manager = null;
			}
			var person = new Person
			{
				Name = name,
				Position = position,
				TableNumber = tableNumber,
				Order = order,
				Manager = manager,
				IsRemoved = false
			};
			Context.Entry(person).State = EntityState.Added;
			Context.SaveChanges();
		}

		public void Update(int id, string name, string position, int tableNumber, string order, int managerTableNumber)
		{
			var manager = new Person();
			if(managerTableNumber!=0)
			{
				manager = Context.Persons.Single(p => p.TableNumber == managerTableNumber);
			}
			else
			{
				manager = null;
			}
			var person = Context.Persons.Single(p => p.Id == id);
			person.Name = name;
			person.Position = position;
			person.TableNumber = tableNumber;
			person.Order = order;
			person.Manager = manager;
			Context.Entry(person).State = EntityState.Modified;
			Context.SaveChanges();
		}
	}
}
