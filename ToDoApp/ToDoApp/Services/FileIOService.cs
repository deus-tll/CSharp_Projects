using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
	class FileIOService
	{
		private readonly string PATH;

		public FileIOService(string path)
		{
			PATH = path;
		}

		public BindingList<TodoModel> LoadData()
		{
			bool fileExists = File.Exists(PATH);
			if (!fileExists)
			{
				File.CreateText(PATH).Dispose();
				return new BindingList<TodoModel>();
			}
			using(StreamReader reader = File.OpenText(PATH))
			{
				string fileText = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
			}
		}


		public void SaveData(object todoDataList)
		{
			using(StreamWriter writer = File.CreateText(PATH))
			{
				string output = JsonConvert.SerializeObject(todoDataList);
				writer.WriteLine(output);
			}
		}
	}
}
