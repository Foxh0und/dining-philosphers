using System;
using MySemaphore = DiningPhilosophers.Semaphore;

namespace DiningPhilosophers
{
	/// <summary>
	/// Mutex, a synchronization object used to control a permission via a signal token.
	/// Acquire is used to receive a permission from the Mutex. When no permission is available available, the thread will wait.
	/// Release is used to give a permission to the semaphore, will wake threads waiting for permission so they one can acquire. Will throw an exception if an attempt
	///         is made to release more than one permission.
	/// </summary>
	public class Mutex: MySemaphore
	{
		/// <summary>
		/// Constructor, initialises the mutex with a single permission.
		/// </summary>
		public Mutex (): base( 1 )
		{}

		/// <summary>
		/// Releases a single token into the Semaphore. Pulses threads waiting for a token. Will throw an exception if an attempt is made to release more than one permisson.
		/// </summary>
		/// <param name="aNumberOfTokens">Re</param>
		public override void Release( int aNumberOfTokens = 1 )
		{
			lock (this)
			{
				if( aNumberOfTokens != 1 )
				{	
					throw new System.ArgumentException ("Can only release one token");
				}
				if( base.fCount == 1 )
				{
					throw new System.IndexOutOfRangeException ("Mutex already has permission");
				}

				base.Release( aNumberOfTokens );
			}
		}
	}
}

