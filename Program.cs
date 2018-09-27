using System;
using System.Threading;

using MyMutex = DiningPhilosophers.Mutex;
using MySemaphore = DiningPhilosophers.Semaphore;

namespace DiningPhilosophers
{
	class MainClass
	{
		public static void Main( string[] args )
		{
			MySemaphore lConductor = new MySemaphore( 4 );
			
			MyMutex fForkOne = new MyMutex();
			MyMutex fForkTwo = new MyMutex();
			MyMutex fForkThree = new MyMutex();
			MyMutex fForkFour = new MyMutex();
			MyMutex fForkFive= new MyMutex();

			Philosopher lPhilosopherOne = new Philosopher( "Philosopher One" , fForkFive , fForkOne, lConductor );
			Philosopher lPhilosopherTwo = new Philosopher( "Philosopher Two" , fForkOne , fForkTwo, lConductor );
			Philosopher lPhilosopherThree = new Philosopher( "Philosopher Three" , fForkTwo, fForkThree, lConductor );
			Philosopher lPhilosopherFour = new Philosopher( "Philosopher Four" , fForkThree, fForkFour, lConductor );
			Philosopher lPhilosopherFive = new Philosopher( "Philosopher Five" , fForkFour, fForkFive , lConductor );

			lPhilosopherOne.Start();
			lPhilosopherTwo.Start();
			lPhilosopherThree.Start();
			lPhilosopherFour.Start();
			lPhilosopherFive.Start();
		}
	}
}
