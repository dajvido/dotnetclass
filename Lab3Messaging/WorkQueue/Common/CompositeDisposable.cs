using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class CompositeDisposable : IDisposable
    {
        private List<IDisposable> _innerList;

        public CompositeDisposable(params IDisposable[] disposables)
            : this((IEnumerable<IDisposable>) disposables)
        {
        }

        public CompositeDisposable(IEnumerable<IDisposable> disposables)
        {
            _innerList = disposables == null
                ? new List<IDisposable>()
                : disposables.Where(d => d != null).ToList();
        }

        public void Add(IDisposable d)
        {
            if (d == null) throw new ArgumentNullException("d");

            _innerList.Add(d);
        }

        public void Dispose()
        {
            foreach (var disposable in _innerList)
            {
                disposable.Dispose();
            }
        }
    }
}