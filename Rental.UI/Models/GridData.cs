using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.UI.Models
{
    public class GridData<T>
    {
        public GridData()
            : this(new List<T>(), 0)
        { }

        public GridData(IEnumerable<T> rows, int total)
        {
            Rows = rows;
            Total = total;
        }

        /// <summary>
        /// 获取或设置 行数据
        /// </summary>
        public IEnumerable<T> Rows { get; set; }

        /// <summary>
        /// 获取或设置 数据行数
        /// </summary>
        public int Total { get; set; }
    }
}