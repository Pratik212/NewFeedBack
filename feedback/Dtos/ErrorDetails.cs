﻿using Newtonsoft.Json;

namespace FoodDelivery.Dtos
{
    /// <summary>
    ///     Error Details
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        ///     Status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        ///     Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}