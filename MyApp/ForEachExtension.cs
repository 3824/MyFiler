using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public static class ForEachExtension
    {
        public static IEnumerable<T> FolderForEach<T>(this IEnumerable<T> sequence, Action<Exception> handler)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (handler == null) throw new ArgumentNullException("handler");
            var mover = sequence.GetEnumerator();
            bool more = true;
            while (more)
            {
                bool isOk = false;
                try
                {
                    more = mover.MoveNext();
                    isOk = true;
                }
                catch (UnauthorizedAccessException ae)
                {
                    // do nothing
                    continue;
                }
                catch (Exception e)
                {
                    handler(e);
                    yield break;
                }
                if (isOk)
                {
                    yield return mover.Current;
                }
            }
        }
    }
}
