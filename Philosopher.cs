using System;
using System.Threading;
using MyMutex = DiningPhilosophers.Mutex;
using MySemaphore = DiningPhilosophers.Semaphore;


namespace DiningPhilosophers
{
	public class Philosopher:ActiveObject
	{
		private MyMutex fLeftFork;
		private MyMutex fRightFork;
		private MySemaphore fSemaphore;

		public Philosopher (string aName, MyMutex aLeftFork, MyMutex aRightFork, MySemaphore aSemaphore ): base (aName)
		{
			fLeftFork = aLeftFork;
			fRightFork = aRightFork;
			fSemaphore = aSemaphore;
		}

		private void Think()
		{
			Console.WriteLine( "{0} is thinking.", fActiveObjectThread.Name);
			Thread.Sleep(5000);
		}

		private void Pickup()
		{
			fSemaphore.Acquire();
			fLeftFork.Acquire();
			fRightFork.Acquire();
		}

		private void Eat()
		{
			Console.WriteLine( "{0} is eating.", fActiveObjectThread.Name);
			Thread.Sleep(3000);
		}

		private void PutDown()
		{
			fLeftFork.Release();
			fRightFork.Release();
			fSemaphore.Release();
			Console.WriteLine( "{0} has put down it's forks.", fActiveObjectThread.Name);
		}

		public override void Run()
		{
			while( true ) 
			{
				Think();
				Pickup();
				Eat();
				PutDown();
			}
		}
	}
}

