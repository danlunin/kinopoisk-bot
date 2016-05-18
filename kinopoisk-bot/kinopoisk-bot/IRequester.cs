using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinopoisk_bot {
    //Этот класс обращается к сайту и возвращает нужную информацию
    interface IRequester {
        IData Search(ICriteria criteria);
    }
}
