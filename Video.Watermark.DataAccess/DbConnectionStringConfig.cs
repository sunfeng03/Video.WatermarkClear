﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Video.Watermark.DataAccess
{
    public class DbConnectionStringConfig
    {
        private static DbConnectionStringConfig _default;

        /// <summary>
        /// 默认配置对象
        /// </summary>
        public static DbConnectionStringConfig Default { get { return _default; } }

        /// <summary>
        /// MyShopDB
        /// </summary>
        public string MyShopConnectionString { get; set; }
    
        /// <summary>
        /// 初始化配置对象
        /// </summary>
        public static void InitDefault(DbConnectionStringConfig defaultConfig)
        {
            _default = defaultConfig;
        }
    }
}
