﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace UnitTestProject4
{
	[Binding]
	public class Hook
	{
		[BeforeTestRun]
		public void BeforeRunTest()
		{
			context
		}
	}
}
