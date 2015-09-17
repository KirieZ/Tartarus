// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	/// <summary>
	/// Basic server class, extended by every server
	/// to allow GUI access to them.
	/// </summary>
	public abstract class ServerBase
	{
		public abstract void Load();
		public abstract void Start();
	}
}
