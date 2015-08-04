using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MessageQueue.Api.Models
{
	public class MyDbContext : DbContext
	{
		public MyDbContext()
			: base("AppDB")
		{
		}

		public DbSet<Job> Jobs { get; set; }

	}
}