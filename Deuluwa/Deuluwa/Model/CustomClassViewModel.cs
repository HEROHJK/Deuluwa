using System;
using System.Collections.Generic;
using System.Text;

namespace Deuluwa
{
    class CustomClassViewModel
    {
        public List<CustomClass> customClasses { get; set; }
        public CustomClassViewModel()
        {
            customClasses = new CustomClass().GetCustomClasses();
        }
    }
}
