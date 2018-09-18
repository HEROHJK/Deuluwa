using System;
using System.Collections.Generic;
using System.Text;

namespace Deuluwa
{
    public interface ToastMessage
    {
        void LongToast(string message);
        void ShortToast(string message);
    }
}
