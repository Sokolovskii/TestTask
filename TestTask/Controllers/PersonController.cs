using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using NLog;
using NLog.Web;
using System;
using TestTask.Entity;
using TestTask.Report;


namespace TestTask.Controllers
{
	[Route("api/person")]
	[Produces("application/json")]
	public class PersonController : Controller
	{
		public readonly Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

		private readonly IPersonRepository Repository;
		public PersonController(IPersonRepository repository)
		{
			logger.Info("Создан контроллер для API");
			this.Repository = repository;
		}

		[HttpGet]
		public List<Person> GetPersons()
		{
			logger.Info("Начало запроса людей для таблицы");
			try
			{
				var persons = Repository.GetAllPersons();
				logger.Info("Люди для таблицы готовы");
				return persons;
			}
			catch (Exception ex)
			{
				logger.Info($"Список не создан, возникла ошибка: {ex}");
				return null;
			}

		}
		
		[HttpGet("Managers")]
		public List<Person> GetStructedManagers(int id)
		{
			logger.Info("Начало запроса начальников");
			try
			{
				var managers = Repository.GetStructedManagers(id);
				logger.Info("Начальники готовы");
				return managers;
			}
			catch (Exception ex)
			{
				logger.Info($"Список не создан, возникла ошибка: {ex}");
				return null;
			}

		}

		[HttpPost("Add")]
		public bool AddPerson(string name, string position, int tableNumber, string order, int managerTableNumber)
		{
			logger.Info("Начало создания записи");
			try
			{
				Repository.Add(name, position, tableNumber, order, managerTableNumber);
				logger.Info("запись создана");
				return true;
			}
			catch(Exception ex)
			{
				logger.Info($"Запись не создана, возникла ошибка: {ex}");
				return false;
			}
			
			
		}

		[HttpPost("Update")]
		public bool UpdatePerson(int id, string name, string position, int tableNumber, string order, int managerTableNumber)
		{
			logger.Info("Начало изменения записи");
			try
			{
				Repository.Update(id, name, position, tableNumber, order, managerTableNumber);
				logger.Info("Запись изменена");
				return true;
			}
			catch (Exception ex)
			{
				logger.Info($"Запись не изменена, возникла ошибка: {ex}");
				return false;
			}

		}
		
		[HttpPost]
		public bool Delete(int id)
		{
			logger.Info($"Начало удаления записи с Id: {id}");
			try
			{
				Repository.Delete(id);
				logger.Info($"Запись с Id: {id} успешно удалена");
				return true;
			}
			catch (Exception ex)
			{
				logger.Error($"Случилось исключение типа: {ex}, исправьте");
				return false;
			}
		}

		[HttpGet("excel")]
		public IActionResult CreateExcelFile()
		{
			logger.Info("Начало создания Excel файла");
			try
			{
				var persons = Repository.GetAllPersons();
				var byteTable = ExcelRender.GetByteTable(persons);
				logger.Info("Файл Excel успешно создан");

				return File(
				fileContents: byteTable,
				contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				fileDownloadName: "Persons.xlsx");
				
			}
			catch(Exception ex)
			{
				logger.Info($"Файл excel не создан, произошла ошибка: {ex}");
				return View("Error");
			}
		}

	}
}