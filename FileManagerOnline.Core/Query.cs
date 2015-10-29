﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Core
{
    public class Query
    {
        public ECommand Command { get; set; }

        public string KeyValue { get; set; }

        public string ConnectionString { get; set; }

        public string Section { get; set; }
    }

    public enum ECommand
    {
        get,
        check
    }
}
