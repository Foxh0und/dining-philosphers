using System;
using System.Threading;

namespace DiningPhilosophers
{
	/// Abstract Active Object class.
	/// Takes a thread name, and an option flag to start upon construction, does not by default(false).
	public abstract class ActiveObject
	{
		protected Thread fActiveObjectThread;

		public ActiveObject( string aName, bool start = false )
		{
			fActiveObjectThread = new Thread ( Run );
			fActiveObjectThread.Name = aName;

			if (start)
				fActiveObjectThread.Start ();
		}
			
		/// Starts the Active Object thread.
		public void Start()
		{
			fActiveObjectThread.Start ();
		}

		/// Abstract method designed to be overridden by Active Object's children.
		public abstract void Run();
	}
}
