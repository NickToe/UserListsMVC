using System.Diagnostics;

namespace UserListsMVC;

public static class CustomStopwatch
{
  private static Stopwatch? stopwatch = null;
  private static int counter = 0;
  private static TimeSpan timeSpan = TimeSpan.Zero;

  public static void Start()
  {
    stopwatch = Stopwatch.StartNew();
  }

  public static void Stop()
  {
    stopwatch.Stop();
    counter++;
    timeSpan += stopwatch.Elapsed;
    if (counter % 10 == 0)
    {
      Console.WriteLine("Average time after {0} tries: {1}", counter, timeSpan / counter);
      counter = 0;
      timeSpan = TimeSpan.Zero;
    }
  }
}
