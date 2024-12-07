using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public interface Operations
    {
        public void Insert() { }
        public void Update() { }
        public void Delete() { }
        public void Clean(Control control) { }
    }
}