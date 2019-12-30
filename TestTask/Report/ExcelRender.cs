using OfficeOpenXml;
using System;
using System.Collections.Generic;
using TestTask.Models;

namespace TestTask.Report
{
	public static class ExcelRender
	{
		public static byte[] GetByteTable(List<Person> persons)
		{
			using (var excelPackage = new ExcelPackage())
			{
				//Подписываем таблицу
				excelPackage.Workbook.Properties.Author = "Соколовский Александр";
				excelPackage.Workbook.Properties.Title = "Заголовок документа";
				excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
				excelPackage.Workbook.Properties.Created = DateTime.Now;

				//Создаем лист таблицы
				var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

				//Подписываем заголовки к столбцам
				worksheet.Cells[1, 1].Value = "Имя сотрудника";
				worksheet.Cells[1, 2].Value = "Должность сотрудника";
				worksheet.Cells[1, 3].Value = "Номер сотрудника";
				worksheet.Cells[1, 4].Value = "Отдел сотрудника";
				worksheet.Cells[1, 5].Value = "Имя руководителя";

				//Заполняем таблицу
				int count = 2;
				foreach (var p in persons)
				{
					var manager = string.Empty;
					if (p.Manager != null)
					{
						manager = p.Manager.Name;
					}
					
					worksheet.Cells[count, 1].Value = p.Name;
					worksheet.Cells[count, 2].Value = p.Position;
					worksheet.Cells[count, 3].Value = p.TableNumber;
					worksheet.Cells[count, 4].Value = p.Order;
					worksheet.Cells[count, 5].Value = manager;
					count++;
				}

				byte[] bin = excelPackage.GetAsByteArray();
				return bin;
			}
		}
	}
}
