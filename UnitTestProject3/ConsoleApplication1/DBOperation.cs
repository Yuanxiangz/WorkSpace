using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Eze.Ims.Automation.Framework.Common;

namespace ConsoleApplication1
{
	public class DbOperation
	{
		private TestLog log = new TestLog();

		public string ConnectionString { get; set; }

		public DbOperation()
		{
		}

		public DbOperation(string connectionStr)
		{
			ConnectionString = connectionStr;
		}

		#region Public functions

		public object ExecuteScalar(string sql)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					SqlCommand command = new SqlCommand(sql, connection);
					connection.Open();

					return command.ExecuteScalar();
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(sql);
				throw new Exception(string.Format("Error executing the following SQL: {0}. Error Message = {1}", sql, ex.Message));
			}
		}

		public int ExecuteNonQuery(string sql)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					SqlCommand command = new SqlCommand(sql, connection);
					connection.Open();

					return command.ExecuteNonQuery();
				}
			}
			catch
			{
				Trace.WriteLine(sql);
				throw;
			}
		}

		public DataSet GetDataSet(string sql)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					SqlCommand command = new SqlCommand(sql, connection);
					connection.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter { SelectCommand = command };

					DataSet result = new DataSet();

					dataAdapter.Fill(result);

					return result;
				}
			}
			catch (Exception)
			{
				Trace.WriteLine(sql);
				throw;
			}
		}

		public void RunSQLInCommandFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("Run sql command file failed: " + filePath + " cannot be found.");
			}

			Process process = new Process
			{
				StartInfo =
				{
					WorkingDirectory = Path.Combine(
						GetWorkingDirectory(),
						Constants.SqlPath),
					FileName = Constants.RunSQLFileCmd,
					Arguments = GetDbArguments() + " " + Path.GetFileName(filePath),
					CreateNoWindow = false
				}
			};

			process.Start();
			if (!process.HasExited)
			{
				process.WaitForExit();
			}
		}

		#endregion

		private string GetDbArguments()
		{
			string[] connectionStrings = ConnectionString.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			string db = string.Empty, sv = string.Empty, un = string.Empty, pw = string.Empty;

			Array.ForEach(connectionStrings,
				item =>
				{
					string[] pair = item.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

					switch (pair[0].ToLower())
					{
						case Constants.DataSource:
							sv = pair[1];
							break;

						case Constants.InitialCatalog:
							db = pair[1];
							break;

						case Constants.UserID:
							un = pair[1];
							break;

						case Constants.Password:
							pw = pair[1];
							break;
					}
				}
				);

			if (string.IsNullOrWhiteSpace(sv) ||
				string.IsNullOrWhiteSpace(db) ||
				string.IsNullOrWhiteSpace(un) ||
				string.IsNullOrWhiteSpace(pw))
			{
				log.Error(string.Format("Error: there is empty value in connection string. sv: {0}, db: {1}, un: {2}, pw: {3}.",
				sv, db, un, pw));
			}

			string args = db + " " + sv + " " + un + " " + pw; // 1: db, 2: sv, 3: un, 4: pw
			return args;
		}

		private string GetWorkingDirectory()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}
	}
}