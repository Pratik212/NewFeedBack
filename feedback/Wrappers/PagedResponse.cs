﻿namespace FoodDelivery.Wrappers
{
    /// <summary>
    /// PagedResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T> : Response<T>
    {
        /// <summary>
        /// PageNumber
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// TotalRows
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// PagedResponse
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalRows"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PagedResponse(T data,int totalRows ,int pageNumber, int pageSize)
        {
            this.TotalRows = totalRows;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}