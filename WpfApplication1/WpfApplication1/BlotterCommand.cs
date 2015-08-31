using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
	public class BlotterCommand
	{
		public BlotterCommandType CommandType { get; set; }

		public Action CommandHandler { get; set; }

		public BlotterCommandLayOut LayOut { get; set; }
	}

	public enum BlotterCommandType
	{
		Delete,
		Update,
		Create,
		Insert
	}

	public enum BlotterCommandLayOut
	{
		Right,
		Botton
	}
}
