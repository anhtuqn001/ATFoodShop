using Microsoft.AspNetCore.Mvc;
using SalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Food Food { get; set; }
        public string PageTitle { get; set; }
    }
}
