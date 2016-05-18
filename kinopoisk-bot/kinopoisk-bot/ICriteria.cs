using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinopoisk_bot {
    //Интерфейск классов, накаплювающих текущие критерии поиска.
    interface ICriteria {
        //Критерии хранятся в удобном виде

        //Метод применяется при поиске
        //Преобразовывает все критерии в часть http адреса
        string ToHTTP();
    }
}
