using Microsoft.EntityFrameworkCore;

namespace TestTask.Models
{
	public class PersonContext:DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public PersonContext(DbContextOptions<PersonContext> options):base(options)
		{
			Database.EnsureCreated();
		}

		public PersonContext(){}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>().HasData(
				new Person[]
				{
					new Person
					{
						Id=1,
						Name="Конрад Керз",
						Position="Разработчик",
						TableNumber=1000,
						Order="Отдел Фронтэнда",
						IsRemoved=false
					},
					new Person
					{
						Id=2,
						Name="Марнеус Калгар",
						Position="Разработчик",
						TableNumber=1001,
						Order="Отдел Бэкэнда",
						IsRemoved=false
					},
					new Person
					{
						Id=3,
						Name="Робаут Жиллиман",
						Position="Разработчик",
						TableNumber=1002,
						Order="Отдел Фронтэнда",
						IsRemoved=false
					},
					new Person
					{
						Id=4,
						Name="Хорус Луперкаль",
						Position="Директор департамента",
						TableNumber=1003,
						Order="IT-департамент",
						IsRemoved=false
					},
					new Person
					{
						Id=5,
						Name="Леман Русс",
						Position="ТимЛид",
						TableNumber=1004,
						Order="Отдел Фронтэнда",
						IsRemoved=false
					},
					new Person
					{
						Id=6,
						Name="Лион Эль-Джонсон",
						Position="Разработчик",
						TableNumber=1005,
						Order="Отдел Бэкэнда",
						IsRemoved=false
					},
					new Person
					{
						Id=7,
						Name="Магнус Рыжый",
						Position="ТимЛид",
						TableNumber=1006,
						Order="Отдел Бэкэнда",
						IsRemoved=false
					},
				});
		}

	}
}
