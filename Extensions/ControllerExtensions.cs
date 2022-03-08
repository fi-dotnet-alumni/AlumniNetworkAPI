using System;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetworkAPI.Extensions
{
    public static class ControllerExtensions
	{
        /// <summary>
        /// Generates 303 Action result
        /// </summary>
        /// <param name="location">Location you want to be redirected to</param>
        /// <returns>StatusCodeResult 303</returns>
        public static ActionResult SeeOther(this ControllerBase controller, string location)
        {
			controller.Response.Headers.Add("Location", location);
			return new StatusCodeResult(303);
        }
	}
}

