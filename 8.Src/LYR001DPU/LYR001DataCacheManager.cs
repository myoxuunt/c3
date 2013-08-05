
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;


namespace LYR001DPU
{
    internal class LYR001DataCacheManager
    {
        private LYR001DataCache _dataCache = null;

        internal LYR001DataCache GetDataCache()
        {
            if (_dataCache == null ||
                    _dataCache.IsComplete () ||
                    _dataCache.IsTimeout())
            {
                _dataCache = new LYR001DataCache();
            }
            return _dataCache;
        }
    }

}
