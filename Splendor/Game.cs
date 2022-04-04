using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor {
	public class Game {
		public static Game Instance;
		View view;

		public Game(View v) {
			Instance = this;
			view = v;
		}
	}
}
