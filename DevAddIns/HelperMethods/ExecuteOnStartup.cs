using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    class ExecuteOnStartup
    {
        private TranslatorList m_translatorList;
        public ExecuteOnStartup()
        {
            m_translatorList.DisplasyTranslatorOutput();
        }
    }
}
