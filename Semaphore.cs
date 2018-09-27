using System;
using System.Threading;
using System.Diagnostics;

namespace DiningPhilosophers
{
  /// <summary>
  /// Semaphore, a synchronization object used to control permissions via a number of tokens.
  /// Acquire is used to receive a permission from the semaphore. When no permissions are available, the thread will wait.
  /// Release is used to give permission(s) to the semaphore, will wake threads waiting for permission so they can acquire.
  /// TryAcquire is used to acquire a token within a specified time limit.
  /// ForceRelease calls release but can handle the interruption of a thread appropriately.
  /// </summary>
  public class Semaphore
  {
    protected long fCount;
	protected long fThreadCount;

    /// <summary>
    /// Constructor, initialises number of tokens.
    /// </summary>
    /// <param name="aNumberOfTokens"> Number of Tokens to construct Semaphore with. Default is 0</param>
    public Semaphore( int aNumberOfTokens = 0 )
    {
      fCount = aNumberOfTokens;
	  fThreadCount = 0;
    }

    /// <summary>
    /// Release(s) tokens into the Semaphore. Pulses threads waiting for a token.
    /// </summary>
    /// <param name="aNumberOfTokens"> Re</param>
    public virtual void Release( int aNumberOfTokens = 1 )
    {
      lock( this )
      {
		fCount += aNumberOfTokens;
		int lMax = (int)Math.Min( fCount, fThreadCount );
		
		for( int i = 0; i < lMax; i ++ )
			Monitor.Pulse (this);
      }
    }

    /// <summary>
    /// Thread acquires tokens, if none available, waits until one is.
    /// </summary>
	public virtual void Acquire()
    {
		TryAcquire ();
    }
   /// <summary>
   /// Thread acquires permission, if none available, waits (up to a specified time limit) until one is.
   /// </summary>
   /// <returns><c>true</c>, If the thread received permission within specified time limit <c>false</c> otherwise.</returns>
   /// <param name="aTimeLimit">A time limit.</param>
	public virtual bool TryAcquire( int aTimeLimit = -1 )
	{
		lock( this ) 
		{
			Stopwatch lTimer = new Stopwatch();
			int lElapsedTime;
			fThreadCount++;

			try
			{
				while ( fCount == 0 )
				{
					if( aTimeLimit >= 0 )
					{
						lElapsedTime = aTimeLimit - (int)lTimer.ElapsedMilliseconds;
						if (lElapsedTime > 0) 
						{
							if ( Monitor.Wait( this, lElapsedTime ) )
							{
								if( fCount > 0 ) 
								{
									fCount--;
									return true;
								}
							}
							else
								return false;
							}	
						} 
					else
						Monitor.Wait( this );
				}
			}
			finally
			{
				fThreadCount--;
			}
			fCount--;
			return true;
		}
	}
	/// <summary>
	/// Forces the release, even if the thread is interrupted.
	/// Tries to release a token, if interrupt is thrown, catches the interrupt and releases tokens, then interrupts the thread. 
	/// </summary>
	/// <returns><c>true</c>, if release was forced, <c>false</c> otherwise.</returns>
	/// <param name="aNumberOfTokens">A number of tokens.</param>
	public virtual bool ForceRelease(int aNumberOfTokens = 1)
	{
		bool lInterrupt = false;
		while (true)
		{
			try
			{
				Release( aNumberOfTokens );

				if(lInterrupt)
					Thread.CurrentThread.Interrupt();
					
				return true;
			}

			catch(ThreadInterruptedException e)
			{
				lInterrupt = true;
				continue;
			}
		}
	}
  }
}
